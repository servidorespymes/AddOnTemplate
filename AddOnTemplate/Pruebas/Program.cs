namespace Pruebas
{
  
    using SAPbobsCOM;
    using SAPbouiCOM;
    using System.Security;
    using System.Xml;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //Variable Interfaz (Con esto podemos afectar la UI de SAP)
        public static SAPbouiCOM.Application SBO_Application = null;
        public static Comunes.Eventos oEventos = null;
        //Variable UIAPI conexión
        public static SAPbobsCOM.Company oCompany = null;
        public static string[] DocEntryRef = new string[2];
        [STAThread]
        static void Main()
        {
            Comunes.ConexiónSAPB1 oConexion = new Comunes.ConexiónSAPB1();
            Formularios.frmBinLocations oForm = null;
            Formularios.frmEntrada oForme = null;

            if (Comunes.ConexiónSAPB1.oSAPB1appl != null && Comunes.ConexiónSAPB1.oCompanyDIAPI.Connected)
            {
               
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Modulo Conectado", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Success);
                oEventos = new Comunes.Eventos();
                //Comunes.ConexiónSAPB1.oSAPB1appl.MenuEvent += oEventos.OSAPB1appl_MenuEvent;
                CrearMenu();
                oForm = new Formularios.frmBinLocations();
                oForme = new Formularios.frmEntrada();

            }
            GC.KeepAlive(oEventos);
            GC.KeepAlive(oConexion);
            System.Windows.Forms.Application.Run();
        }
        private static void CrearMenu()
        {
            try
            {
                // EVITAR DUPLICADOS
                if (Comunes.ConexiónSAPB1.oSAPB1appl.Menus.Exists("Logistica"))
                    return;

                SAPbouiCOM.MenuCreationParams oCreationPackage =
                    (SAPbouiCOM.MenuCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(
                        SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);

                SAPbouiCOM.MenuItem oMenuItem = Comunes.ConexiónSAPB1.oSAPB1appl.Menus.Item("43520");
                SAPbouiCOM.Menus oMenus = oMenuItem.SubMenus;

                // Menú principal
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                oCreationPackage.UniqueID = "Logistica";
                oCreationPackage.String = "Logística";
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = 15;
                oMenus.AddEx(oCreationPackage);

                // Submenús
                oMenuItem = Comunes.ConexiónSAPB1.oSAPB1appl.Menus.Item("Logistica");
                oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "SeguimientoPedidos";
                oCreationPackage.String = "Seguimiento de Pedidos";
                oMenus.AddEx(oCreationPackage);



                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "EntradaMercancia";
                oCreationPackage.String = "Entrada de Mercancía";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.UniqueID = "SalidaMercancia";
                oCreationPackage.String = "Salida de Mercancía";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.UniqueID = "ListasdePicking";
                oCreationPackage.String = "Listas de Picking";
                oMenus.AddEx(oCreationPackage);

            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error creando menú: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }


    }
}