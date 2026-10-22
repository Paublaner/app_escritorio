import psycopg
from dotenv import load_dotenv
import os


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

load_dotenv()

def obtener_cuentas() -> str:
    lineas = []

    with  psycopg.connect(os.environ["FINANZAS_DSN"]) as conn:
        with conn.cursor() as cur:
            cur.execute(sql)
            filas = cur.fetchall()

        if filas is [] or not filas:
            return "No hay cuentas registradas"

        

        for cuenta, tipo, saldo in filas:
             lineas.append(f"Cuenta: {cuenta}, Tipo: {tipo}, Saldo: {saldo}")

        return "\n".join(lineas)
    
print(obtener_cuentas()) 