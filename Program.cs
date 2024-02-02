using System.Globalization;

namespace EsercizioLezione5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Inserisci i dati del contribuente:");

            Console.Write("\nNome: ");
            string nome = Console.ReadLine();

            Console.Write("Cognome: ");
            string cognome = Console.ReadLine();

            Console.Write("Data di nascita (yyyy-MM-dd): ");
            DateTime dataNascita = DateTime.Parse(Console.ReadLine());
            if (!Contribuente.ControllaDataNascita(dataNascita))
            {
                Console.WriteLine("Data di nascita non valida");
                return;
            }

            Console.Write("Codice Fiscale: ");
            string codiceFiscale = Console.ReadLine();
            if (!Contribuente.ControllaCodiceFiscale(codiceFiscale))
            {
                Console.Write("Il codice fiscale inserito non è valido");
                return;
            }

            Console.Write("Sesso (M/F): ");
            string sesso = Console.ReadLine();
            if (!Contribuente.ControllaSesso(sesso))
            {
                Console.Write("Sesso inserito non valido inserici M o F");
                return;
            }

            Console.Write("Comune di Residenza: ");
            string comuneDiResidenza = Console.ReadLine();

            Console.Write("\nReddito annuale: ");
            double redditoAnnuale = double.Parse(Console.ReadLine());
            if (!Contribuente.ControllaReddito(redditoAnnuale))
            {
                Console.WriteLine("Errore nell'inserimento del reddito");
                return;
            }

            Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneDiResidenza, redditoAnnuale);

            double imposta = contribuente.CalcoloImposta();

            Console.WriteLine("\n=========================================\n");
            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE: \n");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome}, \n" +
                              $"nato il {contribuente.DataNascita: dd/MM/yyyy} ({contribuente.Sesso}), \n" +
                              $"residente in {contribuente.ComuneDiResidenza}, \n" +
                              $"codice fiscale: {contribuente.CodiceFiscale}\n" +
                              $"Reddito dichiarato: € {contribuente.RedditoAnnuale:N2}\n" +
                              $"IMPOSTA DA VERSARE: € {imposta:N2}");
        }
    }

    public class Contribuente
    {
        public string Nome;
        public string Cognome;
        public DateTime DataNascita;
        public string CodiceFiscale;
        public string Sesso;
        public string ComuneDiResidenza;
        public double RedditoAnnuale;

        public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, string sesso, string comuneDiResidenza, double redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale.ToUpper();
            Sesso = sesso.ToUpper();
            ComuneDiResidenza = comuneDiResidenza;
            RedditoAnnuale = redditoAnnuale;
        }

        public double CalcoloImposta()
        {
            double imposta = 0;

            if (RedditoAnnuale <= 15000)
            {
                imposta = RedditoAnnuale * 0.23;
            }
            else if (RedditoAnnuale <= 28000)
            {
                imposta = 3450 + (RedditoAnnuale - 15000) * 0.27;
            }
            else if (RedditoAnnuale <= 55000)
            {
                imposta = 6960 + (RedditoAnnuale - 28000) * 0.38;
            }
            else if (RedditoAnnuale <= 75000)
            {
                imposta = 17220 + (RedditoAnnuale - 55000) * 0.41;
            }
            else
            {
                imposta = 25420 + (RedditoAnnuale - 75000) * 0.43;
            }

            return imposta;
        }

        public static bool ControllaDataNascita(DateTime dataNascita)
        {
            // La data di nascita deve essere in passato 
            return (dataNascita < DateTime.Now);
        }

        public static bool ControllaCodiceFiscale(string codiceFiscale)
        {
            // Controlla se ci sono spazi e se il codice fiscale ha la lunghezza giusta
            return !string.IsNullOrWhiteSpace(codiceFiscale) && codiceFiscale.Length == 16;
        }

        public static bool ControllaSesso(string sesso)
        {
            // Controlla se è stato inserito M o F correttamente
            return (sesso == "M" || sesso == "F" || sesso == "m" || sesso == "f");
        }

        public static bool ControllaReddito(double redditoAnnuale)
        {
            // Il reddito annuale deve essere maggiore di 0
            return redditoAnnuale > 0;
        }
    }
}
