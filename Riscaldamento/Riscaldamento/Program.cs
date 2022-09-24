using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Prova di ingresso: RIscaldamento conveniente
 * Giuriato Simone 5F
 * 24/09/2022
 */
namespace Riscaldamento
{
    class Program
    {
        class Elettrico     //classe padre degli strumenti elettrici
        {
            //attributi
            protected double rendimento;
            protected double nuovaBolletta;
            protected double costoElettrica;
            protected double costoSistema;

            public Elettrico(double rendimento, double costoElettrica, double costoSistema)     //costruttore classe padre
            {
                this.rendimento = rendimento;
                this.costoElettrica = costoElettrica;
                this.costoSistema = costoSistema;

            }
            public virtual double Bolletta(double energia)      //metodo virtual che verrà passato (con virtual) ai figli (ogni metodo di riscaldamento) per calcolare la bolletta
            {
                return nuovaBolletta;
            }

        }
        class Gas           //classe padre degli strumenti a gas
        {   //attributi
            protected double rendimento;
            protected double nuovaBolletta;
            protected double costoGas;
            protected double costoSistema;

            public Gas(double rendimento, double costoGas, double costoSistema)     //costruttore classe padre
            {
                this.rendimento = rendimento;
                this.costoGas = costoGas;
                this.costoSistema = costoSistema;

            }
            public virtual double Bolletta(double energia)          //metodo virtual che verrà passato (con virtual) ai figli (ogni metodo di riscaldamento) per calcolare la bolletta
            {
                return nuovaBolletta;

            }
        }

        class StufaElettrica : Elettrico        //classe figlia  della classe "Elettrico" per la stufa elettrica 
        {
            public StufaElettrica(double rendimento, double costoElettrica, double costoSistema) : base(rendimento, costoElettrica, costoSistema)           //costruttore (richiamo al padre)
            {


            }
            public override double Bolletta(double energia)         //calcola la nuova bolletta per il generatore di calore in questione, i dati arrivano dal main poichè sempre fissi
            {
                nuovaBolletta = (energia / rendimento) * costoElettrica + costoSistema;
                return nuovaBolletta;
            }

        }
        class CaldaiaTradizionale : Gas     //classe figlia della classe "Gas" per la caldaia tradizionale
        {
            public CaldaiaTradizionale(double rendimento, double costoGas, double costoSistema) : base(rendimento, costoGas, costoSistema)          //costruttore (richiamo al padre)
            {

            }
            public override double Bolletta(double energia)      //calcola la nuova bolletta per il generatore di calore in questione, i dati arrivano dal main poichè sempre fissi
            {
                nuovaBolletta = (energia / rendimento) * costoGas + costoSistema;
                return nuovaBolletta;
            }
        }
        class CaldaiaCondensazione : Gas         //classe figlia della classe "Gas" per la caldaia a condensazione
        {
            public CaldaiaCondensazione(double rendimento, double costoGas, double costoSistema) : base(rendimento, costoGas, costoSistema)         //costruttore (richiamo al padre)
            {

            }
            public override double Bolletta(double energia)      //calcola la nuova bolletta per il generatore di calore in questione, i dati arrivano dal main poichè sempre fissi
            {
                nuovaBolletta = (energia / rendimento) * costoGas + costoSistema;
                return nuovaBolletta;
            }


        }
        class PompaEconomica : Elettrico        //classe figlia  della classe "Elettrico" per la pompa di calore economica
        {
            public PompaEconomica(double rendimento, double costoElettrica, double costoSistema) : base(rendimento, costoElettrica, costoSistema)       //costruttore (richiamo al padre)
            {


            }
            public override double Bolletta(double energia)      //calcola la nuova bolletta per il generatore di calore in questione, i dati arrivano dal main poichè sempre fissi
            {
                nuovaBolletta = (energia / rendimento) * costoElettrica + 1000 + costoSistema;
                return nuovaBolletta;
            }

            public double AnniSuccessivi(double costoPompaE)        //calcola la nuova bolletta per il generatore di calore in questione ma togliendo il prezzo di installazione per gli anni successivi, dando un' indicazione minima all'utente 
            {                                                       //double costoPompaE= costo della bolletta calcolata precedentemente della pompa economica
                return costoPompaE - 1000;
            }


        }
        class PompaBuona : Elettrico            //classe figlia  della classe "Elettrico" per la pompa di calore di buon livello
        {
            public PompaBuona(double rendimento, double costoElettrica, double costoSistema) : base(rendimento, costoElettrica, costoSistema)           //costruttore (richiamo al padre)
            {


            }
            public override double Bolletta(double energia)      //calcola la nuova bolletta per il generatore di calore in questione, i dati arrivano dal main poichè sempre fissi
            {
                nuovaBolletta = (energia / rendimento) * costoElettrica + 3000 + costoSistema;
                return nuovaBolletta;
            }

            public double AnniSuccessivi(double costoPompaB)           //calcola la nuova bolletta per il generatore di calore in questione ma togliendo il prezzo di installazione per gli anni successivi, dando un' indicazione minima all'utente 
            {                                                          //double costoPompaB= costo della bolletta calcolata precedentemente della pompa livello buono
                return costoPompaB - 3000;
            }



        }
        static void Main(string[] args)
        {
            bool controllo;     //controllo do while
            double consumoGas = 0;          //consumo SmC inseriti dall'utente (per i dispositivi a gas)
            double consumoEnergia = 0;      //consumo kWh inseriti dall'utente (per i dispositivi a corrente)
            string numeroMenù;          //numero digitato dall'utente per la scelta del menu iniziale
            double costoPE = 0;            //bolletta pompa economica
            double costoPB = 0;             //bolletta pompa livello buono
            double costoSE = 0;              //bolletta stufa elettrica
            double costoCC = 0;              //bolletta caldaia condensazione
            double costoCT = 0;              //bolletta caldaia tradizionale
            double consiglio = 0;              //bolletta più bassa che va consigliata all'utente
            double costolungoPE = 0;           //costo bolletta pompa economica senza l'installazione       (1000 euro)
            double costolungoPB = 0;           // costo bollette pompa buon livello senza l'installazione (3000 euro)
            List<double> bollette = new List<double>();     //lista che conterrà tutti i costi calcolati per ogni generatore di calore

            //INIZIALIZZAZIONE CLASSI
            PompaEconomica pe = new PompaEconomica(2.8, 0.3, 213);      //(rendimento, costo corrente, oneri di sistema)
            PompaBuona pb = new PompaBuona(3.6, 0.3, 213);
            StufaElettrica se = new StufaElettrica(1, 0.3, 213);
            CaldaiaCondensazione cc = new CaldaiaCondensazione(1, 2, 213);  //(rendimento, costo gas, oneri di sistema)
            CaldaiaTradizionale ct = new CaldaiaTradizionale(0.9, 2, 213);

            //COMUNICAZIONE ALL'UTENTE DEL MENU INIZIALE
            do
            {
                Console.WriteLine("\n-------------------------------------------------MENU' DI SCELTA--------------------------------------------------------");
                Console.WriteLine("---Scegli il tuo metodo di riscaldamento:\n" +
                              "--->Premi 1 se possiedi una stufa elettrica;\n" +
                              "--->Premi 2 se possiedi una caldaia tradizionale;\n" +
                              "--->Premi 3 se possiedi una caldaia condensazione;\n" +
                              "--->Premi 4 se possiedi una pompa di calore economica;\n" +
                              "--->Premi 5 se possiedi una pompa di calore di buon livello;\n" +
                              "--->Premi * se vuoi uscire.\n");
                numeroMenù = Console.ReadLine(); //assunzione valore da tastiera
                Console.Clear();
            } while (numeroMenù != "1" && numeroMenù != "2" && numeroMenù != "3" && numeroMenù != "4" && numeroMenù != "5" && numeroMenù != "*");   //controllo che l'utente inserisica solo queste opzioni

            switch (numeroMenù)     //scelta del menù
            {
                case "1": //caso in cui venga premuto 1
                    do
                    {
                        string e;
                        Console.WriteLine("Inserisci quanti kWh di energia elettrica consumi per riscaldarti:\n");
                        e = Convert.ToString(Console.ReadLine());
                        controllo = double.TryParse(e, out consumoEnergia); // Variabile boolena per controllare se il tipo di dato inserito è un double, False: se non è un double, True: se lo è
                    } while (!controllo || consumoEnergia <= 0);   //controllo per la regolarità , non deve essere inserito un consumo minore o uguale a 0 o una stringa 
                    consumoGas = (consumoEnergia / 10.7);       //conversione in gas per il consumo espresso in kWh     1Smc-->10.7kWh
                    //ASSUNZIONE DEI VARI COSTI NELLE VARIABILI IN BASE AL CONSUMO PASSATO NEI METODI PRECEDENTI
                    costoPE = pe.Bolletta(consumoEnergia);
                    costoPB = pb.Bolletta(consumoEnergia);  //dispositivo ad energia elettrica
                    costoCT = ct.Bolletta(consumoGas);      //dispositivo a gas
                    costoCC = cc.Bolletta(consumoGas);
                    costoSE = se.Bolletta(consumoEnergia);
                    costolungoPB = pb.AnniSuccessivi(costoPB);      //bollette effettive delle pompe di calore per gli anni successivi senza il costo aggiuntivo(messo nel primo anno) dell'installazione
                    costolungoPE = pe.AnniSuccessivi(costoPE);
                    //AGGIUNTA DI OGNI COSTO PER OGNI METODO DI RISCALDAMENTO NELLA LISTA bollette
                    bollette.Add(costoSE);
                    bollette.Add(costoPE);
                    bollette.Add(costoPB);
                    bollette.Add(costoCC);
                    bollette.Add(costoCT);
                    //COMUNICAZIONE ALL'UTENTE DI OGNI COSTO DELLE BOLLETTE PER INFORMAZIONE 
                    Console.WriteLine();
                    Console.WriteLine(" ******************** Calcolo bolletta vantaggiosa SOLO per l'ANNO CORRENTE.********************");
                    Console.WriteLine("\nLa tua bolletta attuale (con una stufa elettrica) è: {0} euro.\n", costoSE);
                    Console.WriteLine("Una bolletta con una caldaia tradizionale è : {0} euro. \n ", costoCT);
                    Console.WriteLine("Una bolletta con una caldaia a condensazione è: {0} euro. \n", costoCC);
                    Console.WriteLine("Una bolletta con una pompa di calore economica è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n ", costoPE, costolungoPE);
                    Console.WriteLine("Una bolletta con una pompa di calore di buon livello è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n ", costoPB, costolungoPB);
                    consiglio = bollette.Min();     //bollette.Min trova il valore più basso nella lista bollette. Esso corrisponde alla bolletta più conveniente
                    //SE IL GENERATORE DI CALORE è IL PIù IDONEO VERRà INDICATO COME IDEALE
                    if (consiglio == costoCT)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia tradizionale. ");
                    }
                    else if (consiglio == costoCC)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia a condensazione.");
                    }
                    else if (consiglio == costoPB)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore di buon livello.");

                    }
                    else if (consiglio == costoPE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore economica.");
                    }
                    else if (consiglio == costoSE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una stufa elettrica,\nquindi puoi mantenere l'attuale metodo di riscaldamento.");
                    }
                    else
                    {
                        Console.WriteLine("Errore!!!");
                    }


                    break;
                case "2":       //caso in cui venga premuto 2
                    do
                    {
                        string e;
                        Console.WriteLine("Inserisci quanti SmC di gas metano consumi per riscaldarti:\n");
                        e = Convert.ToString(Console.ReadLine());
                        controllo = double.TryParse(e, out consumoGas);
                    } while (!controllo || consumoGas <= 0);   //controllo per la regolarità 
                    consumoEnergia = consumoGas * 10.7;
                    costoPE = pe.Bolletta(consumoEnergia);
                    costoPB = pb.Bolletta(consumoEnergia);
                    costoCT = ct.Bolletta(consumoGas);
                    costoCC = cc.Bolletta(consumoGas);
                    costoSE = se.Bolletta(consumoEnergia);
                    costolungoPB = pb.AnniSuccessivi(costoPB);
                    costolungoPE = pe.AnniSuccessivi(costoPE);
                    bollette.Add(costoSE);
                    bollette.Add(costoPE);
                    bollette.Add(costoPB);
                    bollette.Add(costoCC);
                    bollette.Add(costoCT);
                    Console.WriteLine();
                    Console.WriteLine(" ******************** Calcolo bolletta vantaggiosa SOLO per l'ANNO CORRENTE ********************");
                    Console.WriteLine("\nUna bolletta con una stufa elettrica è: {0} euro.\n", costoSE);
                    Console.WriteLine("La tua bolletta attuale (con una caldaia tradizionale) è: {0} euro. ", costoCT);
                    Console.WriteLine("Una bolletta con una caldaia a condensazione è: {0} euro. \n", costoCC);
                    Console.WriteLine("Una bolletta con una pompa di calore economica è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n   ", costoPE, costolungoPE);
                    Console.WriteLine("Una bolletta con una pompa di calore di buon livello è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n ", costoPB, costolungoPB);
                    consiglio = bollette.Min();
                    if (consiglio == costoSE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una stufa elettrica.");
                    }
                    else if (consiglio == costoCC)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia a condensazione. ");
                    }
                    else if (consiglio == costoPB)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore di buon livello.");

                    }
                    else if (consiglio == costoPE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore economica.");
                    }
                    else if (consiglio == costoCT)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia tradizionale,\nquindi puoi mantenere l'attuale metodo di riscaldamento.");
                    }
                    else
                    {
                        Console.WriteLine("Errore!!!");
                    }


                    break;
                case "3":       //caso in cui venga premuto 3
                    do
                    {
                        string e;
                        Console.WriteLine("Inserisci quanti SmC di gas metano consumi per riscaldarti:\n");
                        e = Convert.ToString(Console.ReadLine());
                        controllo = double.TryParse(e, out consumoGas);
                        consumoEnergia = consumoGas * 10.7;
                    } while (!controllo || consumoGas <= 0);   //controllo per la regolarità 
                    consumoEnergia = (consumoGas * 10.7);
                    costoPE = pe.Bolletta(consumoEnergia);
                    costoPB = pb.Bolletta(consumoEnergia);
                    costoCT = ct.Bolletta(consumoGas);
                    costoCC = cc.Bolletta(consumoGas);
                    costoSE = se.Bolletta(consumoEnergia);
                    costolungoPB = pb.AnniSuccessivi(costoPB);
                    costolungoPE = pe.AnniSuccessivi(costoPE);
                    Console.WriteLine();
                    Console.WriteLine(" ******************** Calcolo bolletta vantaggiosa SOLO per l'ANNO CORRENTE ********************");
                    Console.WriteLine("\nUna bolletta con una stufa elettrica è: {0} euro.\n", costoSE);
                    Console.WriteLine("Una bolletta con una caldaia tradizionale è : {0} euro. \n", costoCT);
                    Console.WriteLine("La tua bolletta attuale (con una caldaia a condensazione) è: {0} euro. \n", costoCC);
                    Console.WriteLine("Una bolletta con una pompa di calore economica è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n  ", costoPE, costolungoPE);
                    Console.WriteLine("Una bolletta con una pompa di calore di buon livello è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n ", costoPB, costolungoPB);
                    bollette.Add(costoSE);
                    bollette.Add(costoPE);
                    bollette.Add(costoPB);
                    bollette.Add(costoCC);
                    bollette.Add(costoCT);
                    consiglio = bollette.Min();
                    if (consiglio == costoSE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una stufa elettrica.");
                    }
                    else if (consiglio == costoCT)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia tradizionale. ");
                    }
                    else if (consiglio == costoPB)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore di buon livello.");

                    }
                    else if (consiglio == costoPE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore economica.");
                    }
                    else if (consiglio == costoCC)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia a condensazione,\nquindi puoi mantenere l'attuale metodo di riscaldamento.");
                    }
                    else
                    {
                        Console.WriteLine("Errore!!!");
                    }
                    break;

                case "4":        //caso in cui venga premuto 4
                    do
                    {
                        string e;
                        Console.WriteLine("Inserisci quanti kWh di energia elettrica consumi per riscaldarti:\n");
                        e = Convert.ToString(Console.ReadLine());
                        controllo = double.TryParse(e, out consumoEnergia);
                    } while (!controllo || consumoEnergia <= 0);   //controllo per la regolarità 
                    consumoGas = (consumoEnergia / 10.7);
                    costoPE = pe.Bolletta(consumoEnergia);
                    costoPB = pb.Bolletta(consumoEnergia);
                    costoCT = ct.Bolletta(consumoGas);
                    costoCC = cc.Bolletta(consumoGas);
                    costoSE = se.Bolletta(consumoEnergia);
                    costolungoPB = pb.AnniSuccessivi(costoPB);
                    costolungoPE = pe.AnniSuccessivi(costoPE);
                    Console.WriteLine();
                    Console.WriteLine(" ******************** Calcolo bolletta vantaggiosa SOLO per l'ANNO CORRENTE ********************");
                    Console.WriteLine("\nUna bolletta con una stufa elettrica è: {0} euro.\n", costoSE);
                    Console.WriteLine("Una bolletta con una caldaia tradizionale è : {0} euro. \n ", costoCT);
                    Console.WriteLine("Una bolletta con una caldaia a condensazione è: {0} euro. \n ", costoCC);
                    Console.WriteLine("La tua bolletta attuale (con una pompa elettrica economica) è {0}  euro.\n ", costolungoPE);//essendo già installata non occorre vedere la boletta di inizio anno perchè senza costo installazione
                    Console.WriteLine("Una bolletta con una pompa di calore di buon livello è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n ", costoPB, costolungoPB);
                    bollette.Add(costoSE);
                    bollette.Add(costolungoPE);
                    bollette.Add(costoPB);
                    bollette.Add(costoCC);
                    bollette.Add(costoCT);
                    consiglio = bollette.Min();
                    if (consiglio == costoSE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una stufa elettrica.");
                    }
                    else if (consiglio == costoCC)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia a condensazione.");
                    }
                    else if (consiglio == costoCT)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una caldaia tradizionale. ");

                    }
                    else if (consiglio == costoPB)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore di buon livello.");
                    }
                    else if (consiglio == costolungoPE)
                    {
                        Console.WriteLine("Il nostro consiglio è di installare una pompa di calore economica,\nquindi puoi mantenere l'attuale metodo di riscaldamento.");
                    }
                    else
                    {
                        Console.WriteLine("Errore!!! ");
                    }

                    break;

                case "5":            //caso in cui venga premuto 5
                    do
                    {
                        string e;
                        Console.WriteLine("Inserisci quanti kWh di energia elettrica consumi per riscaldarti:\n");
                        e = Convert.ToString(Console.ReadLine());
                        controllo = double.TryParse(e, out consumoEnergia);
                    } while (!controllo || consumoEnergia <= 0);   //controllo per la regolarità 
                    consumoGas = (consumoEnergia / 10.7);
                    costoPE = pe.Bolletta(consumoEnergia);
                    costoPB = pb.Bolletta(consumoEnergia);
                    costoCT = ct.Bolletta(consumoGas);
                    costoCC = cc.Bolletta(consumoGas);
                    costoSE = se.Bolletta(consumoEnergia);
                    costolungoPB = pb.AnniSuccessivi(costoPB);
                    costolungoPE = pe.AnniSuccessivi(costoPE);
                    Console.WriteLine();
                    Console.WriteLine(" ******************** Calcolo bolletta vantaggiosa SOLO per l'ANNO CORRENTE.********************");
                    Console.WriteLine("\nUna bolletta con una stufa elettrica è: {0} euro.\n", costoSE);
                    Console.WriteLine("Una bolletta con una caldaia tradizionale è : {0} euro. \n ", costoCT);
                    Console.WriteLine("Una bolletta con una caldaia a condensazione è: {0} euro. \n ", costoCC);
                    Console.WriteLine("Una bolletta con una pompa di calore economica è: {0} euro.\nPer gli anni successivi la tua bolletta sarà: {1} euro.\n  ", costoPE, costolungoPE);
                    Console.WriteLine("La tua bolletta attuale (con pompa elettrica di buon livello) è: {0} euro\n", costolungoPB);     //essendo già installata non occorre vedere la boletta di inizio anno perchè senza costo installazione

                    Console.WriteLine("Riteniamo che il metodo di riscaldamento attualmente in uso(pompa di calore di buon livello) sia il milgiore."); //dalla proggettazione software appare come sempre ideale la pompa di calore di buon livello
                                                                                                                                                        //in caso già installato.

                    break;
                case "*":       //in caso venga premuto *
                    Environment.Exit(0);        //esce dalla console (comando jolly per uscire)
                    break;



            }
            Console.ReadKey();
        }
    }
}
