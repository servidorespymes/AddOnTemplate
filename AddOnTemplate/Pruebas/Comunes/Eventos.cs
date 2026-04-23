using Pruebas.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Comunes
{
    class Eventos
    {

        #region Constructor

        public Eventos()
        {
            try
            {
                ConexiónSAPB1.oSAPB1appl.AppEvent += OSAPB1appl_AppEvent;
                //Descomentar para grilla logistica, comentar para broker
                ConexiónSAPB1.oSAPB1appl.ItemEvent += OSAPB1appl_ItemEvent;
                //ConexiónSAPB1.oSAPB1appl.
                //ConexiónSAPB1.oSAPB1appl.FormDataEvent += OSAPB1appl_FormDataEvent;
                ConexiónSAPB1.oSAPB1appl.FormDataEvent += OSAPB1appl_FormDataEvent;
                Comunes.ConexiónSAPB1.oSAPB1appl.MenuEvent += OSAPB1appl_MenuEvent;
                Comunes.ConexiónSAPB1.oSAPB1appl.LayoutKeyEvent += OSAPB1appl_LayoutKeyEvent;

            

                FiltroEventos();
            }
            catch (Exception ex)
            {

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Eventos inicializados",
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                //ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void OSAPB1appl_LayoutKeyEvent(ref SAPbouiCOM.LayoutKeyInfo eventInfo, out bool BubbleEvent)
        {

            BubbleEvent = true;
            try
            {
                if (eventInfo.FormUID == "UDO_F_CARGACPE")
                {
                    SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(eventInfo.FormUID);
                    eventInfo.LayoutKey = oForm.DataSources.DBDataSources.Item("@CARGACPE").GetValue("DocEntry", 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OSAPB1appl_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (BusinessObjectInfo.FormUID)
                {

                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OSAPB1appl_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
            $"MenuEvent: {pVal.MenuUID} Before={pVal.BeforeAction}",
            SAPbouiCOM.BoMessageTime.bmt_Short,
            SAPbouiCOM.BoStatusBarMessageType.smt_Warning);


            try
            {

                if (pVal.BeforeAction)
                {
                    //Error creando menú: Menu - Already exists  [66000-68]

                    switch (pVal.MenuUID)
                    {


                        //case "EntradaMercancia":
                        //    //AbrirFormularioSAP("143");
                        //    AbrirFormularioSAP("2305");
                        //    break;

                        //case "SalidaMercancia":
                        //    //AbrirFormularioSAP("140");
                        //    AbrirFormularioSAP("2306");
                        //    break;

                        //case "ListasdePicking":
                        //    //AbrirFormularioSAP("85");
                        //    AbrirFormularioSAP("147");
                        //    break;

                        case "SeguimientoPedidos":
                            AbrirCockpitWeb();
                            break;


                        case "EntradaMercancia":
                            Comunes.ConexiónSAPB1.oSAPB1appl.ActivateMenuItem("2305");
                            break;

                        case "SalidaMercancia":
                            Comunes.ConexiónSAPB1.oSAPB1appl.ActivateMenuItem("2306");
                            break;

                        case "ListasdePicking":
                            Comunes.ConexiónSAPB1.oSAPB1appl.ActivateMenuItem("147");
                            break;


                    }



                }




            }

            catch (Exception ex) {

                // Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText( "MenuEvent detectado: " + pVal.MenuUID,SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
        "MenuEvent: " + pVal.MenuUID + " Before=" + pVal.BeforeAction,
        SAPbouiCOM.BoMessageTime.bmt_Short,
        SAPbouiCOM.BoStatusBarMessageType.smt_Warning);


            }
        }

        private void OSAPB1appl_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                switch (pVal.FormTypeEx)
                {
                    case Formularios.frmBinLocations.StrForm:
                        Formularios.frmBinLocations oForm = new Formularios.frmBinLocations();
                        oForm.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;


                    case Formularios.frmEntrada.frmEnt:
                        Formularios.frmEntrada oForme = new Formularios.frmEntrada();
                        oForme.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;


                    case frmCockpitWeb.StrForm:
                        frmCockpitWeb oCockpit = new frmCockpitWeb();
                        oCockpit.OSAPB1appl_ItemEvent(FormUID, ref pVal, out BubbleEvent);
                        break;
                }

            }
            catch (Exception ex)
            {

                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion

        #region Eventos 
        private void OSAPB1appl_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            try
            {
                //Switch para los diferentes tipos de eventos.
                //De esta forma terminamos la aplicación en cualquiera de todos estos eventos.
                switch (EventType)
                {
                    //Cuando se cierra la aplicación
                    case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                        Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Finalizando Modulo");
                        Application.Exit();
                        break;
                    //Cuando se cambia la fuente o el lenguaje, nuestro formulario debe cargar de nuevo                    
                    case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                        Application.Restart();
                        ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Reiniciando Addon");
                        break;
                }
            }
            catch (Exception ex)
            {

                ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
        #endregion

        #region Formularios


        private void AbrirFormularioSAP(string menuUID)
        {
            Comunes.ConexiónSAPB1.oSAPB1appl.ActivateMenuItem(menuUID);
        }
        private void AbrirCockpitWeb()
        {

            /*
            try
            {
                // Cargar el formulario cockpitWeb desde XML o UDO
                // Si ya está abierto, lo trae al frente
                // Comunes.ConexiónSAPB1.oSAPB1appl.OpenForm(SAPbouiCOM.BoFormObjectEnum.fo_Form, frmCockpitWeb.StrForm, "");

                var oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(frmCockpitWeb.StrForm);
                oForm.Select();
            }
            catch
            {
                // Si falla, intenta abrirlo como menú nativo (por si lo registras como menú SAP)
                Comunes.ConexiónSAPB1.oSAPB1appl.ActivateMenuItem(frmCockpitWeb.StrForm);
            }
            */
            /*
            try
            {
                // Si ya existe, solo lo traemos al frente
                try
                {
                    var f = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(frmCockpitWeb.StrForm);
                    f.Select();
                    return;
                }
                catch { }

                // Crear formulario desde cero
                SAPbouiCOM.FormCreationParams cp =
                    (SAPbouiCOM.FormCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(
                        SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);

                cp.UniqueID = frmCockpitWeb.StrForm;
                cp.FormType = frmCockpitWeb.StrForm;

                SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.AddEx(cp);
                oForm.Title = "Seguimiento de Pedidos";
                oForm.Width = 900;
                oForm.Height = 600;

                // Agregar WebBrowser
                SAPbouiCOM.Item item = oForm.Items.Add("WB1", SAPbouiCOM.BoFormItemTypes.it_WEB_BROWSER);
                item.Left = 5;
                item.Top = 5;
                item.Width = oForm.Width - 10;
                item.Height = oForm.Height - 10;

                SAPbouiCOM.WebBrowser wb = (SAPbouiCOM.WebBrowser)item.Specific;
                wb.Url = "google.com";

                oForm.Visible = true;
            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error creando cockpitWeb: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            */


            try
            {
                // Si ya existe, lo traemos al frente
                try
                {
                    var f = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.Item(frmCockpitWeb.StrForm);
                    f.Select();
                    return;
                }
                catch { }

                // Crear formulario desde cero
                SAPbouiCOM.FormCreationParams cp =
                    (SAPbouiCOM.FormCreationParams)Comunes.ConexiónSAPB1.oSAPB1appl.CreateObject(
                        SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);

                cp.UniqueID = frmCockpitWeb.StrForm;
                cp.FormType = frmCockpitWeb.StrForm;

                SAPbouiCOM.Form oForm = Comunes.ConexiónSAPB1.oSAPB1appl.Forms.AddEx(cp);
                oForm.Title = "Seguimiento de Pedidos";
                oForm.Width = 900;
                oForm.Height = 600;

                // 🔥 Crear el WebBrowser ANTES de que cockpitWeb lo use
                SAPbouiCOM.Item item = oForm.Items.Add("B1SP", SAPbouiCOM.BoFormItemTypes.it_WEB_BROWSER);
                item.Left = 5;
                item.Top = 5;
                item.Width = oForm.Width - 10;
                item.Height = oForm.Height - 10;

                SAPbouiCOM.WebBrowser wb = (SAPbouiCOM.WebBrowser)item.Specific;
                wb.Url = "google.com";

                oForm.Visible = true;
            }
            catch (Exception ex)
            {
                Comunes.ConexiónSAPB1.oSAPB1appl.StatusBar.SetText(
                    "Error creando cockpitWeb: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

        }
        #endregion

        #region Metodos

        private void FiltroEventos()
        {
            //Revisar documentacion SDK
            SAPbouiCOM.EventFilters oFiltros = null;
            SAPbouiCOM.EventFilter oFiltro = null;

            try
            {
                //De esta forma filtro el evento que quiero escuchar en mi aplicación para que no este
                //escuchando todos los itemsevents.
                oFiltros = new SAPbouiCOM.EventFilters();
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);

                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                oFiltro = oFiltros.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
               
                // FILTROS DE MENUS 
                //oFiltro.AddEx(Formularios.FormEntrega.TipoFormulariostr);
                //oFiltro.AddEx(Formularios.FormGastos.strTipoFormulario);
                //oFiltro.AddEx(Formularios.frmLista.strTipoFormulario);
                //oFiltro.AddEx(Formularios.Menu.frmMenu);
                //oFiltro.AddEx(Formularios.frmAutorizaciones.frmName);

                //Esto funciona en cascada, se define el filtro y los formularios que estan debajo, es para donde aplican esos filtros          
                //De esa forma podemos filtrar en que formulario en especifico queremos que se use dicho filtro
                //De esta forma seteamos los filtros
                ConexiónSAPB1.oSAPB1appl.SetFilter(oFiltros);

            }
            catch (Exception ex)
            {

                ConexiónSAPB1.oSAPB1appl.StatusBar.SetText("Eventos()" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        #endregion
    }
}
