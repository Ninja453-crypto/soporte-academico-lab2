using System;
using System.Collections.Generic;

List<Solicitud> solicitudes = new List<Solicitud>();
bool salir = false;

while (!salir)
{
    MostrarMenu();
    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine()?.Trim();

    switch (opcion)
    {
        case "1":
            RegistrarSolicitud(solicitudes);
            break;
        case "2":
            MostrarListado(solicitudes);
            break;
        case "3":
            Console.WriteLine("\nSaliendo del programa... ¡Hasta luego!");
            salir = true;
            break;
        default:
            Console.WriteLine("\nOpción no válida. Intente de nuevo.");
            break;
    }
}

// ==========================================
// FUNCIONES Y MÓDULOS
// ==========================================

void MostrarMenu()
{
    Console.WriteLine("\n--- SISTEMA DE SOPORTE ACADÉMICO ---");
    Console.WriteLine("1. Registrar nueva solicitud");
    Console.WriteLine("2. Mostrar solicitudes registradas");
    Console.WriteLine("3. Salir");
}

void RegistrarSolicitud(List<Solicitud> lista)
{
    if (lista.Count >= 3)
    {
        Console.WriteLine("\n[Aviso] Ya ha alcanzado el mínimo de 3 solicitudes registradas.");
    }

    Console.WriteLine("\n--- Registro de Solicitud ---");
    string codigo = ValidarTextoObligatorio("Ingrese Código de estudiante (mín. 5 caracteres): ", 5);
    string nombre = ValidarTextoObligatorio("Ingrese Nombre del estudiante: ", 3);
    string tipo = ValidarTipoConsulta();
    string descripcion = ValidarTextoObligatorio("Ingrese una breve descripción: ", 5);

    string prioridad = CalcularPrioridad(tipo);

    Solicitud nuevaSolicitud = new Solicitud
    {
        Codigo = codigo,
        Nombre = nombre,
        Tipo = tipo,
        Descripcion = descripcion,
        Prioridad = prioridad
    };

    lista.Add(nuevaSolicitud);

    MostrarResumenSolicitud(codigo, nombre, tipo, descripcion, prioridad);
}

string ValidarTextoObligatorio(string mensaje, int minLongitud)
{
    while (true)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(entrada) && entrada.Length >= minLongitud)
        {
            return entrada;
        }
        Console.WriteLine($"Error: El campo no puede estar vacío y debe tener al menos {minLongitud} caracteres.");
    }
}

string ValidarTipoConsulta()
{
    string[] tiposValidos = { "matrícula", "pagos", "constancia", "plataforma", "otro" };
    while (true)
    {
        Console.Write("Tipo de consulta (matrícula, pagos, constancia, plataforma, otro): ");
        string tipo = Console.ReadLine()?.Trim().ToLower();

        foreach (string item in tiposValidos)
        {
            if (item == tipo)
            {
                return tipo;
            }
        }
        Console.WriteLine("Error: Tipo de consulta no válido. Intente nuevamente.");
    }
}

string CalcularPrioridad(string tipoConsulta)
{
    if (tipoConsulta == "pagos" || tipoConsulta == "plataforma")
    {
        return "Alta";
    }
    else if (tipoConsulta == "matrícula" || tipoConsulta == "constancia")
    {
        return "Media";
    }
    else
    {
        return "Baja";
    }
}

void MostrarResumenSolicitud(string codigo, string nombre, string tipo, string descripcion, string prioridad)
{
    Console.WriteLine("\n========================================");
    Console.WriteLine("RESUMEN DE LA SOLICITUD REGISTRADA");
    Console.WriteLine("========================================");
    Console.WriteLine($"Código Estudiante : {codigo}");
    Console.WriteLine($"Nombre            : {nombre}");
    Console.WriteLine($"Tipo Consulta     : {tipo}");
    Console.WriteLine($"Descripción       : {descripcion}");
    Console.WriteLine($"Prioridad Asignada: {prioridad}");
    Console.WriteLine("========================================");
}

void MostrarListado(List<Solicitud> lista)
{
    if (lista.Count == 0)
    {
        Console.WriteLine("\nNo hay solicitudes registradas aún.");
        return;
    }

    Console.WriteLine($"\n--- LISTADO DE SOLICITUDES ({lista.Count}) ---");
    for (int i = 0; i < lista.Count; i++)
    {
        Console.WriteLine($"\nSolicitud #{i + 1}");
        MostrarResumenSolicitud(
            lista[i].Codigo,
            lista[i].Nombre,
            lista[i].Tipo,
            lista[i].Descripcion,
            lista[i].Prioridad
        );
    }
}

// ESTRUCTURA DE DATOS
struct Solicitud
{
    public string Codigo;
    public string Nombre;
    public string Tipo;
    public string Descripcion;
    public string Prioridad;
}