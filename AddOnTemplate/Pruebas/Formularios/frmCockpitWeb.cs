using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Formularios
{
    public class frmCockpitWeb
    {

        #region Variables
        public const string StrForm = "B1SP";   // ID del formulario UDO/UDF o XML
        private SAPbouiCOM.Form oForm = null;
        private SAPbouiCOM.Item oWebBrowserItem = null;
        private SAPbouiCOM.WebBrowser oWebBrowser = null;
        #endregion

        #region Constructor
        public frmCockpitWeb() { }
        #endregion

        #region ItemEvent
        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.FormTypeEx != StrForm)
                    return;

                // 🔥 Inicializar SOLO cuando el formulario ya está completamente cargado
                if (!pVal.BeforeAction && pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
                {
                    InicializarFormulario(FormUID);
                    return;
                }

                if (!pVal.BeforeAction)
                {
                    switch (pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                            // Botones dentro del cockpit
                            break;
                    }
                }
                else
                {
                    switch (pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                            AjustarWebBrowser();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error cockpitWeb: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }


        #endregion

        #region Métodos internos

        private void InicializarFormulario(string FormUID)
        {
            oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);

            try
            {
                // Intentar obtener el item; si no existe, salimos sin romper nada
                SAPbouiCOM.Item itemWB;

                try
                {
                    // Usa el ID REAL del WebBrowser: WB1 o B1SP
                    itemWB = oForm.Items.Item("B1SP");   // o "WB1" si ese es el correcto
                }
                catch
                {
                    // El item aún no existe o el ID no coincide
                    return;
                }

                oWebBrowserItem = itemWB;
                oWebBrowser = (SAPbouiCOM.WebBrowser)oWebBrowserItem.Specific;

                oWebBrowser.Url = "https://www.google.com";

                AjustarWebBrowser();

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Cockpit Web inicializado",
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inicializando cockpit: " + ex.Message);
            }
        }



        private void AjustarWebBrowser()
        {
            if (oForm == null || oWebBrowserItem == null)
                return;

            try
            {
                oForm.Freeze(true);

                // Ajustar tamaño al formulario
                oWebBrowserItem.Width = oForm.Width - 10;
                oWebBrowserItem.Height = oForm.Height - 50;

                oForm.Freeze(false);
            }
            catch
            {
                oForm.Freeze(false);
            }
        }

        #endregion

    }
}
