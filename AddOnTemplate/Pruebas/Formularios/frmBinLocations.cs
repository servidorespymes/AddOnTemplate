using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Formularios
{
    public class frmBinLocations
    {
        #region 
        public const string StrForm = "1470000006";
        private SAPbouiCOM.Form oForm = null;
        private SAPbouiCOM.Matrix oMatrix = null;
        #endregion

        public frmBinLocations() { }

        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.BeforeAction)
                {
                    switch (pVal.EventType)
                    {
                        case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                            oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(pVal.FormUID);
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("1470000019").Specific;
                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("1470000003").Cells.Item(1).Specific).Value = "300";



                   

                            //((SAPbouiCOM.EditText)oMatrix.Columns.Item("1470000007").Cells.Item(1).Specific).Value = "Demo demo 300";


                            Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("::: Entro a Form :::");

                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
