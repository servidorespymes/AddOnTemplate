using Pruebas.Comunes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Formularios
{
    class frmEntrada
    {
        #region Atributos
        public const string frmEnt = "143";
        private SAPbouiCOM.Form oForm = null;
        private SAPbouiCOM.Matrix oMatrix = null;

        #endregion

        public frmEntrada()
        {

        }

        public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {

                switch (pVal.FormTypeEx)
                {
                    case "143": // OPDN - Entrada de mercancías
                         HandleOPDN(FormUID, ref pVal, out BubbleEvent);
                        //ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("OPDN EVENT DETECTADO");
                        //ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("FormTypeEx: " + pVal.FormTypeEx);
                        return;
                        break;

                    case Formularios.frmBinLocations.StrForm:
                        new Formularios.frmBinLocations().OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;

                    //case Formularios.frmEntrada.frmEnt:
                    //    new Formularios.frmEntrada().OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                    //    break;
                }

                /*
                // Detectar formulario OPDN (Entrada de mercancías)
                if (pVal.FormTypeEx == "143") // 143 = OPDN
                {
                    if (!pVal.BeforeAction)
                    {
                        switch (pVal.EventType)
                        {
                            case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                                SetDefaultComment(FormUID);
                                break;

                            case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                                // Si quieres que también funcione al hacer clic en un botón específico
                                if (pVal.ItemUID == "1") // Botón Añadir/Actualizar
                                    SetDefaultComment(FormUID);
                                break;
                        }
                    }
                }

                // Resto de tu router
                switch (pVal.FormTypeEx)
                {
                    case Formularios.frmBinLocations.StrForm:
                        new Formularios.frmBinLocations().OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;

                    case Formularios.frmEntrada.frmEnt:
                        new Formularios.frmEntrada().OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                }
                */



            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(ex.Message);
            }
        }
        //public void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        //{
        //    BubbleEvent = true;
        //    try
        //    {
        //        if (pVal.BeforeAction)
        //        {
        //            switch (pVal.EventType)

        //            {

        //                case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
        //                    SetDefaultComment(FormUID);
        //                    break;

        //                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
        //                    if (pVal.ItemUID == "253000596") // tu botón
        //                        SetDefaultComment(FormUID);
        //                    break;


        //                    /*
        //                    case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
        //                        oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(pVal.FormUID);
        //                        SetDefaultComment(oForm);
        //                        //((SAPbouiCOM.EditText)oForm.Items.Item("46").Specific).Value = "Comentario por defecto";
        //                        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("::: Entro a Form :::");
        //                        break;

        //                    case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
        //                        if (!pVal.BeforeAction && pVal.ItemUID == "253000596")
        //                        {
        //                            oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(pVal.FormUID);
        //                            ((SAPbouiCOM.EditText)oForm.Items.Item("46").Specific).Value = "Comentario por defecto";
        //                        }
        //                        break;
        //                        */

        //                    //case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
        //                    //    if (pVal.ItemUID == "253000596")
        //                    //    {

        //                    //        oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(pVal.FormUID);
        //                    //        oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("1470000019").Specific;
        //                    //        ((SAPbouiCOM.EditText)oMatrix.Columns.Item("1470000003").Cells.Item(1).Specific).Value = "300";


        //                    //        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("::: Entro a Form :::");
        //                    //    }
        //                    //    break;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        //private void SetDefaultComment(SAPbouiCOM.Form oForm)
        //{
        //    try
        //    {
        //        string defaultComment = "Comentario automático - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //        // Detecta el tipo de formulario
        //        string formType = oForm.TypeEx;

        //        switch (formType)
        //        {


        //            /*
        //            case "139": // Orden de Venta
        //                oForm.DataSources.DBDataSources.Item("ORDR").SetValue("Comments", 0, defaultComment);
        //                break;

        //            case "140": // Entrega
        //                oForm.DataSources.DBDataSources.Item("ODLN").SetValue("Comments", 0, defaultComment);
        //                break;


        //            case "143": // Entrega
        //                oForm.DataSources.DBDataSources.Item("OPDN").SetValue("Comments", 0, defaultComment);
        //                break;

        //            case "133": // Factura
        //                oForm.DataSources.DBDataSources.Item("OINV").SetValue("Comments", 0, defaultComment);
        //                break;
        //        }

        //        oForm.Update();
        //        */

        //    }
        //    catch (Exception ex)
        //    {
        //        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
        //            "Error al asignar comentario: " + ex.Message,
        //            SAPbouiCOM.BoMessageTime.bmt_Short,
        //            SAPbouiCOM.BoStatusBarMessageType.smt_Error
        //        );
        //    }
        //}
        public void HandleOPDN(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            bool comentarioOPDNSeteado = false;

            try
            {
                if (!pVal.BeforeAction)
                {
                    //switch (pVal.EventType)
                    //{
                    //    case SAPbouiCOM.BoEventTypes.et_GOT_FOCUS:
                    //        // Solo poner comentario si el formulario está en modo Añadir
                    //        SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);

                    //        if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    //        {
                    //            SetDefaultComment(FormUID);
                    //            // 1) Lo logueas para ver que entra aquí
                    //            Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    //                $"OPDN ADD_MODE - Evento: {pVal.EventType}",
                    //                SAPbouiCOM.BoMessageTime.bmt_Short,
                    //                SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    //        }
                    //        break;

                    //    case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE:
                    //        SetDefaultComment(FormUID);
                    //        break;

                    //    case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    //        if (pVal.ItemUID == "1") // Botón Añadir/Actualizar
                    //            SetDefaultComment(FormUID);
                    //        break;
                    //}

                    SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(FormUID);



                    // Solo nos interesa cuando el formulario está en modo Añadir
                    if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        // 1) Lo logueas para ver que entra aquí
                        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                            $"OPDN ADD_MODE - Evento: {pVal.EventType}",
                            SAPbouiCOM.BoMessageTime.bmt_Short,
                            SAPbouiCOM.BoStatusBarMessageType.smt_Warning
                        );

                        // 2) Solo lo hacemos una vez por formulario
                        if (!comentarioOPDNSeteado &&
                            (pVal.EventType == SAPbouiCOM.BoEventTypes.et_GOT_FOCUS ||
                             pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE /*|| pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD*/) )
                        {
                            SetDefaultComment(FormUID);
                            //SetDefaultUdfs(FormUID);
                            comentarioOPDNSeteado = true;
                        }
                    }
                    else
                    {
                        // Si cambia de modo, reseteamos el flag
                        comentarioOPDNSeteado = false;
                    }





                }


            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error OPDN: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error
                );
            }
        }

        public void SetDefaultComment(string formUID)
        {
            try
            {
                SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(formUID);
                //oForm.Freeze(true);
                // Campo Comentario en documentos estándar = Item "46"
                // ANEXA VALOR AL REFATNUMBER
                SAPbouiCOM.EditText txtComentario =(SAPbouiCOM.EditText)oForm.Items.Item("14").Specific;
                txtComentario.Value = "ABCD123456";

                //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("U_B1LEANLOG_AREA_COMPRAS_ESTADOS", 0, "GESTIONADO COMPRAS");

                //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("Comments", 0, "Entrada generada automáticamente");



                //Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                //    "Comentario asignado automáticamente",
                //    SAPbouiCOM.BoMessageTime.bmt_Short,
                //    SAPbouiCOM.BoStatusBarMessageType.smt_Success
                //);

                //oForm.Freeze(false);
            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error comentario: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error
                );
            }
        }

        private void SetDefaultUdfs(string formUID)
        {
            SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(formUID);



            // Solo si ya hay proveedor cargado
            string cardCode = oForm.DataSources.DBDataSources.Item("OPDN").GetValue("CardCode", 0).Trim();

            if (string.IsNullOrEmpty(cardCode))
                return; // SAP aún no ha inicializado la cabecera

            oForm.Freeze(true);

            oForm.DataSources.DBDataSources.Item("OPDN")
                 .SetValue("U_B1LEANLOG_AREA_COMPRAS_ESTADOS", 0, "GESTIONADO COMPRAS");

            oForm.Freeze(false);



            //oForm.Freeze(true);

            //SAPbouiCOM.EditText txtGestionadoVentas = (SAPbouiCOM.EditText)oForm.Items.Item("3").Specific;
            //txtGestionadoVentas.Value = "GESTIONADO COMPRAS";


            //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("U_B1LEANLOG_AREA_COMPRAS_ESTADOS", 0, "GESTIONADO COMPRAS");

            // UDF tipo texto
            //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("U_B1LEANLOG_Facturado", 0, "Y");

            // UDF tipo número
            //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("U_B1LEANLOG_COMENTARIOS_VENTAS", 0, "GESTIONADO VENTAS");

            // UDF tipo fecha (IMPORTANTE: formato YYYYMMDD)
            //oForm.DataSources.DBDataSources.Item("OPDN").SetValue("U_MiFecha", 0, DateTime.Now.ToString("yyyyMMdd"));

            oForm.Freeze(false);
        }

        private static void minimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);

        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);


    }
}
