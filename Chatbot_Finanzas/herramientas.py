from langchain_tavily import TavilySearch
import psycopg
from dotenv import load_dotenv
import os
from langchain_core.tools import tool

sql = '''
SELECT "Nombre", "Tipo", "Saldo"
FROM "Cuentas"
ORDER BY "Nombre";
'''
TIPOS_CUENTA = {
    0 : "Banco",
    1 : "Efectivo",
    2: "Ahorro",
    3: "Credito"
}

SQL_RESUMEN_MENSUAL = '''
SELECT to_char("Fecha", 'YYYY-MM') AS mes,
       SUM(CASE WHEN "Tipo" = 0 THEN "Importe" ELSE 0 END) AS ingresos,
       SUM(CASE WHEN "Tipo" = 1 THEN "Importe" ELSE 0 END) AS gastos
FROM "Movimientos"
WHERE "CuentaId" = %(cuenta)s
  AND "Fecha" >= date_trunc('month', current_date) - make_interval(months => %(meses)s - 1)
GROUP BY mes
ORDER BY mes;
'''

SQL_CUENTAS_ANALISIS = '''
SELECT "Id", "Nombre", "Tipo"
FROM "Cuentas"
WHERE %(nombre)s::text IS NULL OR "Nombre" ILIKE %(nombre)s
ORDER BY "Nombre";
'''

SQL_PRIMER_MOVIMIENTO = '''
SELECT MIN("Fecha") AS primera_fecha,
(date_trunc('month', current_date) - make_interval(months => %(meses)s - 1))::date AS inicio_periodo
FROM "Movimientos"
WHERE "CuentaId" = %(cuenta)s;
'''

SQL_GASTOS_CATEGORIA = '''
SELECT c."Nombre" AS categoria,
       SUM(m."Importe") AS total,
       COUNT(*) AS num_gastos
FROM "Movimientos" m
JOIN "Categorias" c ON c."Id" = m."CategoriaId"
WHERE m."CuentaId" = %(cuenta)s
  AND m."Tipo" = 1
  AND m."Fecha" >= date_trunc('month', current_date) - make_interval(months => %(meses)s - 1)
GROUP BY c."Nombre"
ORDER BY total DESC;
'''

SQL_LISTA_GASTOS = '''
SELECT m."Fecha", c."Nombre" AS categoria, m."Descripcion", m."Importe"
FROM "Movimientos" m
JOIN "Categorias" c ON c."Id" = m."CategoriaId"
WHERE m."CuentaId" = %(cuenta)s
  AND m."Tipo" = 1
  AND m."Fecha" >= date_trunc('month', current_date) - make_interval(months => %(meses)s - 1)
ORDER BY m."Fecha" DESC
LIMIT 100;
'''

SQL_PRESUPUESTOS = '''
SELECT c."Nombre" AS categoria, p."Anio", p."Mes", p."Limite",
       COALESCE(SUM(m."Importe"), 0) AS gastado
FROM "Presupuestos" p
JOIN "Categorias" c ON c."Id" = p."CategoriaId"
LEFT JOIN "Movimientos" m
       ON m."CategoriaId" = p."CategoriaId"
      AND m."Tipo" = 1
      AND EXTRACT(YEAR FROM m."Fecha") = p."Anio"
      AND EXTRACT(MONTH FROM m."Fecha") = p."Mes"
WHERE make_date(p."Anio", p."Mes", 1) >= date_trunc('month', current_date) - make_interval(months => %(meses)s - 1)
  AND make_date(p."Anio", p."Mes", 1) <= current_date
GROUP BY c."Nombre", p."Anio", p."Mes", p."Limite"
ORDER BY p."Anio", p."Mes", c."Nombre";
'''

load_dotenv()

buscador = TavilySearch(
    max_results=5,
    topic="general",
    country="spain",
    time_range="year",
    search_depth="basic"
)

@tool
def obtener_cuentas() -> str:
    """
    Devuelve todas las cuentas del usuario con su nombre, su tipo (Banco, Efectivo,
    Ahorro o Credito) y su saldo actual en euros. Úsala cuando pregunten cuánto dinero 
    tienen, qué cuentas tienen o el saldo de alguna cuenta.

    """
    lineas = []

    with  psycopg.connect(os.environ["FINANZAS_DSN"]) as conn:
        with conn.cursor() as cur:
            cur.execute(sql)
            filas = cur.fetchall()

        if not filas:
            return "No hay cuentas registradas"

        
        for cuenta, tipo, saldo in filas:
             lineas.append(f"Cuenta: {cuenta}, Tipo: {TIPOS_CUENTA[tipo]}, Saldo: {saldo}€")

        return "\n".join(lineas)



def analizar_cuenta(cur, cuenta, meses) -> list[str]:
    lineas = []
    cur.execute(SQL_RESUMEN_MENSUAL, {"cuenta": cuenta, "meses": meses})
    filas = cur.fetchall()

    if not filas:
        return [f"Sin movimientos en los últimos {meses} meses"]

    for mes , ingresos ,gastos in filas:
        lineas.append(f"Resumen por mes: {mes}, Ingresos: {ingresos:.2f}€, Gastos: {gastos:.2f}€, Diferencia: {ingresos-gastos:.2f}€")

    return lineas

@tool
def analizar_gastos(nombre_cuenta: str | None = None, meses: int = 3) -> str:
    """
    Analiza los gastos del usuario en los últimos meses. Por cada cuenta devuelve los
    ingresos y gastos de cada mes, los gastos agrupados por categoría y la lista de gastos
    con su descripción; al final, los presupuestos de cada categoría comparados con lo gastado.
    Úsala siempre antes de dar consejos para ahorrar o gastar menos.

    Args:
        nombre_cuenta: nombre de una cuenta concreta. Déjalo vacío para analizar todas
            las cuentas; úsalo solo si el usuario pregunta por una cuenta en particular.
        meses: número de meses hacia atrás a analizar, contando el actual. Por defecto 3.
    """
    with  psycopg.connect(os.environ["FINANZAS_DSN"]) as conn:
        with conn.cursor() as cur:
            cur.execute(SQL_CUENTAS_ANALISIS, {"nombre": nombre_cuenta})
            cuentas = cur.fetchall()
            if not cuentas:
                return "No existe ninguna cuenta con ese nombre"
            lineas = []
            for cuenta_id, nombre, tipo in cuentas:
                lineas.append(f"\n=== Cuenta: {nombre} ({TIPOS_CUENTA[tipo]}) ===")
                cur.execute(SQL_PRIMER_MOVIMIENTO, {"cuenta": cuenta_id, "meses": meses})
                primera_fecha, inicio_periodo = cur.fetchone()

                if primera_fecha is None:
                    lineas.append(f"Cuenta {nombre}: sin movimientos registrados.")
                    continue

                if primera_fecha > inicio_periodo:
                    lineas.append(
                        f"AVISO: la cuenta {nombre} solo tiene datos desde el "
                        f"{primera_fecha:%d/%m/%Y}, no de los últimos {meses} meses pedidos."
                    )

                lineas.extend(analizar_cuenta(cur, cuenta_id, meses))

                cur.execute(SQL_GASTOS_CATEGORIA, {"cuenta": cuenta_id, "meses": meses})
                categoria_gastos = cur.fetchall()
                lineas.append("Gastos por categoría:")
                for categoria, total, num_gastos in  categoria_gastos:
                    lineas.append(f"Categoría: {categoria}, Total: {total:.2f}€, Numero gastos: {num_gastos}")

                cur.execute(SQL_LISTA_GASTOS, {"cuenta": cuenta_id, "meses": meses})
                lista_gastos = cur.fetchall()
                lineas.append("Lista de gastos:")
                for fecha, categoria, descripcion, importe in lista_gastos:
                    lineas.append(f"Fecha: {fecha:%d/%m/%Y}, Categoria: {categoria}, Descripcion: {descripcion}, Importe: {importe:.2f}€")

            cur.execute(SQL_PRESUPUESTOS, {"meses": meses})
            presupuestos = cur.fetchall()
            lineas.append("\n=== Presupuestos (todas las cuentas) ===")
            if not presupuestos:
                lineas.append("No hay presupuestos definidos en este periodo.")
            for categoria, anio, mes, limite, gastado in presupuestos:
                inicio = f"Presupuesto {categoria} {mes:02d}/{anio}: límite {limite:.2f}€, gastado {gastado:.2f}€"
                if gastado > limite:
                    lineas.append(f"{inicio} -> SE PASA en {gastado - limite:.2f}€")
                else:
                    lineas.append(f"{inicio} -> dentro, quedan {limite - gastado:.2f}€")

            return "\n".join(lineas)


@tool
def buscar_ofertas(consulta: str) -> str:
    """
    Busca en internet tarifas y ofertas actuales en Madrid para encontrar alternativas
    más baratas a un gasto fijo del usuario. Úsala solo después de analizar_gastos,
    para gastos como luz, gas, fibra, móvil, seguros, gimnasio o suscripciones.
    No la uses para inversiones, préstamos ni productos financieros.

    Args:
        consulta: búsqueda corta y genérica en español sobre el tipo de servicio,
            sin importes ni datos del usuario. Ejemplo: "tarifa fibra 600Mb barata Madrid".
    """
    try:
        respuesta = buscador.invoke({"query": consulta})
    except Exception:
        return "No se han podido buscar ofertas en este momento"

    resultado = respuesta.get("results", [])

    if not resultado:
        return "No se encontraron ofertas para esa búsqueda"

    lineas = []

    for r in resultado:
        lineas.append(f"Título: {r['title']}\nEnlace: {r['url']}\nResumen: {r['content'][:300]}")

    return "\n\n".join(lineas)