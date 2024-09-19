import psycopg2

"""
from dotenv import dotenv_values
env = dotenv_values()
"""

conn_string = "postgresql://mirunaiosub24:0gWc9RYMPsmF@ep-icy-recipe-50031502.eu-central-1.aws.neon.tech/travelmenow_app?sslmode=require"
conn = psycopg2.connect(conn_string)


def get_poi(name):
    connection = None
    try:
        connection = psycopg2.connect(conn_string)
        cursor = connection.cursor()

        query = f"select * from public.\"Landmarks\" where \"Landmarks\".\"Name\"='{name}'"
        cursor.execute(query)
        result = cursor.fetchone()
        cursor.close()

        return result
    except psycopg2.DatabaseError as e:
        print(e)
    finally:
        if connection:
            connection.close()


def get_opening_hours(poi_id):
    connection = None
    try:
        connection = psycopg2.connect(conn_string)
        cursor = connection.cursor()
        query = f"select \"Schedules\".\"Day\", \"Schedules\".\"Opening\", \"Schedules\".\"Closing\" from public.\"Schedules\" where \"Schedules\".\"PointOfInterestId\"={poi_id}"
        cursor.execute(query)
        result = cursor.fetchall()
        cursor.close()

        return result
    except psycopg2.DatabaseError as e:
        print(e)
    finally:
        if connection:
            connection.close()


