using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        static int saldo = 1000;
        static object bloqueo = new object();
        static void Main(string[] args)
        {
            Console.WriteLine($"Saldo inicial: {saldo}");

            Thread cliente1 = new Thread(RetirarDinero);
            Thread cliente2 = new Thread(RetirarDinero);
            Thread cliente3 = new Thread(RetirarDinero);
            Thread cliente4 = new Thread(RetirarDinero);
            Thread cliente5 = new Thread(RetirarDinero);


            cliente1.Start();
            cliente2.Start();
            cliente3.Start();
            cliente4.Start();
            cliente5.Start();

            cliente1.Join();
            cliente2.Join();
            cliente3.Join();
            cliente4.Join();
            cliente5.Join();


            Console.WriteLine("Transacciones finalizadas.");
        }

        static void RetirarDinero()
        {
            Random random = new Random();
            int retiro = random.Next(10, 101); 

            lock (bloqueo)
            {
                Console.WriteLine($"Cliente {Thread.CurrentThread.ManagedThreadId} está intentando retirar {retiro}...");

                if (saldo >= retiro)
                {
                    Thread.Sleep(1000);
                    saldo -= retiro;
                    Console.WriteLine($"Cliente {Thread.CurrentThread.ManagedThreadId} ha retirado {retiro}. Transacción exitosa.");
                    Console.WriteLine($"Saldo restante: {saldo}");
                }
                else
                {
                    Console.WriteLine($"Cliente {Thread.CurrentThread.ManagedThreadId} intentó retirar {retiro}, pero no hay suficiente saldo. Transacción fallida.");
                    Console.WriteLine($"Saldo actual: {saldo}");
                }
            }
        }
    }
}
