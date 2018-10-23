using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack //Lares Dominguez Brandon
{
    class Program
    {      
        static void Main(string[] args)
        {           
            BlackJack Partida = new BlackJack();//Aqui se crea la clase BlackJack y se empieza la partida
            Partida.Juego();
        }
    }
    public class Carta//Clase carta para caracteristicas de la carta.
    {
        public string NomCar { get; set; }//Atributos de la carta, nombre y su valor,
        private int valCar;
        public int Valor
        {
            get//Aqui es para realizar una pregunta de los valores
            {
                if (valCar == 0)//Si es 0 el valor, entonces es un AS
                {
                    bool conti = true;
                    Console.Write("\nValor de AS: 1 u 11 ");//Valor de AS, 1 u 11
                    while (conti)
                    { 
                        try//Aqui es para que solo puedan escoger uno de esos 2 numeros.
                        {
                            string op = Console.ReadLine();
                            if (op != "1" && op != "11")
                                throw new Exception();
                            if (op == "1")
                                valCar = 1;
                            else
                                valCar = 11;
                            conti = false;
                        }
                        catch
                        {
                            Console.WriteLine("1 u 11");
                            conti = true;
                        }
                    }
                }
                return valCar;//Si no es AS, devuelve el valor.
            }
            set { valCar = value; }
        }
        public Carta(string name, int value)//Constructor de la clase
        {
            Valor = value;
            NomCar = name;
        }
    }
    public class BlackJack//Clase BlackJack
    {
       
        private List<Carta> Crear()//Metodo que regrese las cartas.
        {
           
            List<Carta> Car = new List<Carta>();//Creamos la lista
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string symbol = "♥";//Simbolo de corazon
            for (int o = 0; o < 4; o++)//Loop para simbolos
            {
                switch (o)//Aqui se van cambiando los simbolos.
                {
                    case 2:
                        symbol = "♦";
                        break;
                    case 3:
                        symbol = "♣";
                        break;
                    case 4:
                        symbol = "♠";
                        break;
                }
                for (int i = 1; i < 14; i++)//Loop para las cartas dentro de cada simbolo.
                {
                    if (i < 11 && i != 1)//Si tienen valor diferente de 1 u 11
                        Car.Add(new Carta(i + " " + symbol, i));//Aqui se agrega su nombre y valor de la carta.
                    else//Si no es asi
                    {
                        switch (i)//Aqui se separa y se le agrega la letra y su valor
                        {
                            case 1://Si es AS, 0 para identificarla luego.
                                Car.Add(new Carta("A " + symbol, 0));
                                break;
                            case 11://J, Q, K quedan asi
                                Car.Add(new Carta("J " + symbol, 10));
                                break;
                            case 12:
                                Car.Add(new Carta("Q " + symbol, 10));
                                break;
                            case 13:
                                Car.Add(new Carta("K " + symbol, 10));
                                break;
                        }
                    }
                }
            }
            return Car;//Se regresa la lista con todas las cartas.
        }

        private Queue<Carta> Rev()//Aqui se revuelve la baraja y un Queue con las cartas revueltas.
        {
            List<Carta> paqueteN = Crear();//Se crea la lista y se ingresan las cartas con el metodo Crear.
            Queue<Carta> cartasR = new Queue<Carta>();//Se inicia un Queue con una variable Random, con un int para guardar temporalmente el random.
            Random tempo = new Random();
            int tempRandom;
            for (int i = 0; i < 52; i++)//For con todas las cartas.
            {
                tempRandom = tempo.Next(paqueteN.Count);//Se genera un numero al azar conforme cuantas cartas hay en la lista.
                cartasR.Enqueue(paqueteN[tempRandom]);//Se saca la posicion de la carta definida por el random y guarda en el Queue
                paqueteN.RemoveAt(tempRandom);//Se borra la carta dentro de la lista.
            }
            return cartasR;//Regresa un Queue con las cartas barajeadas.
        }
        private void Prom(int gan, int per)//Para promedio de partidas ganadas y perdidas.
        {
            Console.WriteLine("\nJugaste: {0} Partidas \nGanaste: {1}\nPerdiste: {2}", gan + per, gan, per);
        }
        public void Juego()//Aqui se juegan las partidas
        {
            Queue<Carta> cartas = Rev();//Se llaman las cartas barajeadas y se mete en un Queue.
            int Puntuacion, cartasS, gan = 0, per = 0;//Se definen las variables para jugar.
            bool conpreg, conjue = true;//Y variables para seguir jugando o no.
            Carta carta;
            while (conjue)//Continua el juego hasta que se decida.
            {
                Console.Clear();//En caso de seguir, se reinica todo.
                Puntuacion = 0;
                cartasS = 0;
                conpreg = true;

                Console.WriteLine("Partida: {0}", gan + per + 1);//Indica partidas sumando ganadas y perdidas.
                for (int i = 0; i < 5; i++)//For para sacar max 5 cartas
                {
                    carta = cartas.Dequeue();//Saca una carta y conforme vayamos sacando, va sumando.
                    ++cartasS;
                    Console.WriteLine("\nCarta: {0} \n{1}", i + 1, carta.NomCar);//Escribe que carta es
                    Puntuacion = carta.Valor + Puntuacion;//Suma los valores y mostramos.
                    Console.WriteLine("Puntuacion: " + Puntuacion);
                    Console.ReadKey();
                    if (Puntuacion == 21 || cartasS == 5)//Cuando puntuacion es 25 o sacamos 5 cartas, se gana.
                    {
                        if (Puntuacion == 21)
                            Console.WriteLine("\nFelicidades!\nConseguiste 21 puntos");
                        else
                            Console.WriteLine("\nFelicidades!\nConseguiste sacar 5 cartas");
                        gan++;//Se suma a partidas ganadas.
                        break;
                    }
                    if (Puntuacion > 21)//Pasados los 21 se acaba la partida
                    {
                        Console.WriteLine("\nPasaste el limite de puntuacion, suerte la proxima vez!");
                        per++;//Se suma a partidas perdidas y acaba la partida
                        break;
                    }
                }
                Console.WriteLine("\nDeseas jugar nuevamente? :\nSi(1) / No(2)");//Pregunta si deseamos jugar otra vez.
                while (conpreg)
                {
                    try
                    {
                        string opc = Console.ReadLine();
                        if (opc != "1" && opc != "2")
                            throw new Exception();
                        else
                            if (opc == "1")
                            conjue = true;
                        else
                            conjue = false;
                        conpreg = false;
                    }
                    catch
                    {
                        Console.WriteLine("Si(1) o No(2)...");
                        conpreg = true;
                    }
                }
            }//Muestra partidas, ganadas y perdidas
            Prom(gan, per);
            Console.ReadKey();
        }
    }
}



