using System.Configuration;
using NivelStocareDate;
using System;

namespace Program
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER = "NumeFisier";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "";
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;


            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareContacteMem();
                    case "txt":
                        return new AdministareContacteFisier(caleCompletaFisier + "." + formatSalvare);
                }
            }
            return null;
        }


    }
}