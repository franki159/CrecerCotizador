namespace System.Infrastructure.Utils.ToolsUtilities
{
    public static class Constants
    {
        public static class MESSAGE
        {
            public const string PROCESS_SATISFACTORILY = "Proceso Realizado Satisfactoriamente";

        }

        //public static Dictionary<int, string> listaRequerida = new Dictionary<int, string>()
        //{
        //    { 5, "POLIZA"   },
        //    { 6, "CERTIFICADO"},
        //    { 7, "FECHAEMISION"},
        //    { 8, "FECHAINICIOVIGENCIA"},
        //    { 9, "FECHAFINVIGENCIA"},
        //    { 10, "PRIMANETA"  },
        //    { 11, "PRIMAIGV"   },
        //    { 12, "PRIMATOTAL"   },
        //    { 13, "CORREO"  },
        //    { 14, "ASUNTOCORREO"  }
        //};

        public static class TypesOfValues
        {
            public const string Info = "Info";
            public const string Error = "Error";
        }

        public static class DocumentTypes
        {
            public const string FIEL_CUMPLIMIENTO = "FC";
            public const string ADELANTO_DIRECTO = "AD";
            public const string ADELANTO_MATERIAL = "AM";
        }
        public static class TemplateTypes
        {
            public const Int32 FIEL_CUMPLIMIENTO = 1;
            public const Int32 ADELANTO_DIRECTO = 2;
            public const Int32 ADELANTO_MATERIAL = 3;
        }

    }




}
