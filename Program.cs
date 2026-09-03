using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AMFO.Uninter.SistemaBancario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("          SISTEMA BANCARIO - REGISTROS            ");
            Console.WriteLine("==================================================\n");

            // --- 1. INSTANCIA DE BANCO ---
            Banco banco = new Banco(1, "Banco Santander", "SUC-001");
            Console.WriteLine("=== INFORMACIÓN DEL BANCO ===");
            banco.MostrarInformacion();

            // --- 2. CREACIÓN DE 3 OBJETOS CLIENTE ---
            Cliente cliente1 = new Cliente(1, "Ángela Mariana Flores Ortiz", "FOA800101ABC", "Av. Palmira 10", "7771234567");
            Cliente cliente2 = new Cliente(2, "Carlos Mendoza", "MEDC920315XYZ", "Calle Matamoros 45", "7779876543");
            Cliente cliente3 = new Cliente(3, "Sofia Ramírez", "RAMS850620LMN", "Av. Plan de Ayala 12", "7775554433");

            Console.WriteLine("\n=== INFORMACIÓN DE CLIENTES ===");
            cliente1.MostrarInformacion();
            cliente2.MostrarInformacion();
            cliente3.MostrarInformacion();

            // --- 3. CREACIÓN DE OBJETOS CUENTA (SOBRECARGA DE CONSTRUCTORES) ---
       
            Cuenta cuenta1 = new Cuenta("CTA-1001", 15400.50m, "Débito", DateTime.Now.AddYears(-2));
            Cuenta cuenta2 = new Cuenta("CTA-1002", "Ahorro", DateTime.Now.AddYears(-1));
            Cuenta cuenta3 = new Cuenta("Ahorro");

            Console.WriteLine("\n=== INFORMACIÓN DE CUENTAS (SOBRECARGA APLICADA) ===");
            cuenta1.MostrarInformacion();
            cuenta2.MostrarInformacion();
            cuenta3.MostrarInformacion();

            // --- 4. CREACIÓN DE OBJETOS MOVIMIENTO ---
            Movimiento mov1 = new Movimiento(101, DateTime.Now, "Depósito", 5000.00m, "Depósito en efectivo");
            Movimiento mov2 = new Movimiento(102, DateTime.Now, "Retiro", 1200.00m, "Retiro en cajero automático");

            Console.WriteLine("\n=== INFORMACIÓN DE MOVIMIENTOS ===");
            mov1.MostrarInformacion();
            mov2.MostrarInformacion();

            // --- 5. CREACIÓN DE OBJETOS TARJETA ---
            Tarjeta tarjeta1 = new Tarjeta("4152-3134-5678-9012", DateTime.Now.AddYears(3), "123", "4321", "Activa");
            Tarjeta tarjeta2 = new Tarjeta("5520-9876-5432-1098", DateTime.Now.AddYears(2), "456", "8765", "Bloqueada");

            Console.WriteLine("\n=== INFORMACIÓN DE TARJETAS ===");
            tarjeta1.MostrarInformacion();
            tarjeta2.MostrarInformacion();

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }

    // ==========================================
    // CLASE: CLIENTE
    // ==========================================
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Cliente(int idCliente, string nombre, string rfc, string direccion, string telefono)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Rfc = rfc;
            Direccion = direccion;
            Telefono = telefono;
        }

        public void Registrar()
        {
            Console.WriteLine($"Cliente {Nombre} registrado correctamente.");
        }

        public void ActualizarDatos()
        {
            Console.WriteLine($"Datos del cliente {Nombre} actualizados.");
        }

        public List<Cuenta> ConsultarCuentas()
        {
            return new List<Cuenta>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"[ID: {IdCliente}] Cliente: {Nombre} | RFC: {Rfc} | Direccion: {Direccion} | Tel: {Telefono}");
        }
    }

    // ==========================================
    // CLASE: CUENTA
    // ==========================================
    public class Cuenta
    {
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public string TipoCuenta { get; set; }
        public DateTime FechaApertura { get; set; }

        // 1. Constructor Completo (Con saldo inicial)
        public Cuenta(string numeroCuenta, decimal saldo, string tipoCuenta, DateTime fechaApertura)
        {
            NumeroCuenta = string.IsNullOrWhiteSpace(numeroCuenta) ? "CTA-GENERICA" : numeroCuenta;
            Saldo = saldo < 0 ? 0 : saldo;
            TipoCuenta = tipoCuenta;
            FechaApertura = fechaApertura;
        }

        // 2. Constructor Sin Saldo Inicial (Inicia por defecto en $0.0)
        public Cuenta(string numeroCuenta, string tipoCuenta, DateTime fechaApertura)
            : this(numeroCuenta, 0.0m, tipoCuenta, fechaApertura)
        {
        }

        // 3. Constructor Indicando únicamente el Tipo de Cuenta (Asigna valores por defecto)
        public Cuenta(string tipoCuenta)
            : this("CTA-PENDIENTE", 0.0m, tipoCuenta, DateTime.Now)
        {
        }

        public void Depositar(decimal monto)
        {
            Saldo += monto;
            Console.WriteLine($"Se depositaron ${monto:N2}. Saldo actual: ${Saldo:N2}");
        }

        public bool Retirar(decimal monto)
        {
            if (monto <= Saldo)
            {
                Saldo -= monto;
                Console.WriteLine($"Retiro exitoso de ${monto:N2}. Saldo actual: ${Saldo:N2}");
                return true;
            }
            Console.WriteLine("Fondos insuficientes para realizar el retiro.");
            return false;
        }

        public decimal ConsultarSaldo()
        {
            return Saldo;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"[Cuenta: {NumeroCuenta}] Tipo: {TipoCuenta} | Saldo: ${Saldo:N2} | Apertura: {FechaApertura.ToShortDateString()}");
        }
    }

    // ==========================================
    // CLASE: BANCO
    // ==========================================
    public class Banco
    {
        public int IdBanco { get; set; }
        public string Nombre { get; set; }
        public string CodigoSucursal { get; set; }

        public Banco(int idBanco, string nombre, string codigoSucursal)
        {
            IdBanco = idBanco;
            Nombre = nombre;
            CodigoSucursal = codigoSucursal;
        }

        public Cuenta AbrirCuenta(Cliente cliente)
        {
            Cuenta nuevaCuenta = new Cuenta("Débito");
            Console.WriteLine($"Cuenta abierta exitosamente para {cliente.Nombre}");
            return nuevaCuenta;
        }

        public void RegistrarCliente(Cliente cliente)
        {
            Console.WriteLine($"Cliente {cliente.Nombre} registrado en el banco {Nombre}.");
        }

        public Cliente BuscarCliente(int idCliente)
        {
            return new Cliente(idCliente, "Cliente Encontrado", "XXXX000000XXX", "Direccion Registrada", "0000000000");
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"[Banco ID: {IdBanco}] {Nombre} | Sucursal: {CodigoSucursal}");
        }
    }

    // ==========================================
    // CLASE: MOVIMIENTO
    // ==========================================
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }

        public Movimiento(int idMovimiento, DateTime fechaHora, string tipoMovimiento, decimal monto, string concepto)
        {
            IdMovimiento = idMovimiento;
            FechaHora = fechaHora;
            TipoMovimiento = tipoMovimiento;
            Monto = monto;
            Concepto = concepto;
        }

        public void RegistrarMovimiento()
        {
            Console.WriteLine($"Movimiento #{IdMovimiento} registrado exitosamente.");
        }

        public string ObtenerDetalle()
        {
            return $"Movimiento #{IdMovimiento} - {TipoMovimiento}: ${Monto:N2} ({Concepto}) el {FechaHora}";
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"[Mov ID: {IdMovimiento}] Fecha: {FechaHora} | Tipo: {TipoMovimiento} | Monto: ${Monto:N2} | Concepto: {Concepto}");
        }
    }

    // ==========================================
    // CLASE: TARJETA
    // ==========================================
    public class Tarjeta
    {
        public string NumeroTarjeta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Cvv { get; set; }
        public string Nip { get; set; }
        public string Estado { get; set; }

        public Tarjeta(string numeroTarjeta, DateTime fechaVencimiento, string cvv, string nip, string estado)
        {
            NumeroTarjeta = numeroTarjeta;
            FechaVencimiento = fechaVencimiento;
            Cvv = cvv;
            Nip = nip;
            Estado = estado;
        }

        public bool ValidarNIP(string pin)
        {
            return Nip == pin;
        }

        public void Bloquear()
        {
            Estado = "Bloqueada";
            Console.WriteLine($"La tarjeta {NumeroTarjeta} ha sido bloqueada.");
        }

        public void AsignarCuenta(Cuenta cuenta)
        {
            Console.WriteLine($"Tarjeta {NumeroTarjeta} vinculada a la cuenta {cuenta.NumeroCuenta}.");
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"[Tarjeta: {NumeroTarjeta}] Estado: {Estado} | Vence: {FechaVencimiento.ToString("MM/yy")}");
        }
    }
}