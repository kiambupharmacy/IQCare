﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Drawing;

namespace PresentationApp.GreenCard.UserControls
{
    public partial class UserControlKNH_VitalSigns : System.Web.UI.UserControl
    {
        DataSet theDSXML = new DataSet();
        DataView theDVCodeID, theDV;
        DataTable theDT;
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        IKNHStaticForms KNHStatic;

        protected void Page_Load(object sender, EventArgs e)
        {
            //KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            if (!IsPostBack)
            {
                addAttributes();                
                showHideZscores();
            }
            JavaScriptFunctionsOnLoad();

        }
        
        private void JavaScriptFunctionsOnLoad()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BMI", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');", true);
            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HighLightAbnormalValuesAdults", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "show_hide('divshowvitalsign','notvisible');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HighLightAbnormalValuesPeads", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "show_hide('divshowvitalsign','visible');", true);
            }
        }

        private void addAttributes()
        {
            //cblReferredTo.Attributes.Add("OnClick", "TriageCheckBoxHideUnhideOtherSpecialistClinic('" + cblReferredTo.ClientID + "');TriageCheckBoxHideUnhideOtherReferral('" + cblReferredTo.ClientID + "');");

            
            txtHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');");
            txtWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');");
            txtHeight.Attributes.Add("onkeyup", "chkDecimal('" + txtHeight.ClientID + "');");
            txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtWeight.ClientID + "');");
            txtheadcircumference.Attributes.Add("onkeyup", "chkDecimal('" + txtheadcircumference.ClientID + "');");
            //txtweightforheight.Attributes.Add("onkeyup", "chkDecimal('" + txtweightforheight.ClientID + "');");

            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                txtTemp.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtTemp.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtTemp.ClientID + "')");

                txtBPSystolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtBPSystolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPSystolic.ClientID + "');");

                txtBPDiastolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtBPDiastolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPDiastolic.ClientID + "');");

                txtRR.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtRR.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtRR.ClientID + "');");

                txtHR.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtHR.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtHR.ClientID + "');");

                //txtBMI.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                //txtBMI.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
            }
            else
            {
                txtTemp.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtTemp.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtTemp.ClientID + "')");

                txtBPSystolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtBPSystolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPSystolic.ClientID + "');");

                txtBPDiastolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtBPDiastolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPDiastolic.ClientID + "');");

                txtRR.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtRR.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtRR.ClientID + "');");

                txtHR.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                txtHR.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtHR.ClientID + "');");

                //txtBMI.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
                //txtBMI.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtHR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');");
            }


            
            
            
        }

        public void showHideZscores()
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 5)
            {
                lblWALabel.Style.Add("display", "none");
                lblWA.Style.Add("display", "none");
                lblWAClassification.Style.Add("display", "none");

                lblWHLabel.Style.Add("display", "none");
                lblWH.Style.Add("display", "none");
                lblWHClassification.Style.Add("display", "none");

                lblBMIzLabel.Style.Add("display", "none");
                lblBMIz.Style.Add("display", "none");
                lblBMIzClassification.Style.Add("display", "none");
            }
            else
            {
                lblWALabel.Style.Add("display", "block");
                lblWA.Style.Add("display", "block");
                lblWAClassification.Style.Add("display", "block");

                lblWHLabel.Style.Add("display", "block");
                lblWH.Style.Add("display", "block");
                lblWHClassification.Style.Add("display", "block");

                lblBMIzLabel.Style.Add("display", "block");
                lblBMIz.Style.Add("display", "block");
                lblBMIzClassification.Style.Add("display", "block");
            }
        }

        public void calculateZScores()
        {
            double L = 0, M = 0, S = 0, WeightAgeZ = 0, WeightHeightZ = 0, HAz = 0, BMIz = 0, weight = 0, heightInCm = 0, bmi = 0;
            DataSet ZScoreDS = new DataSet();
            IKNHStaticForms KNHS = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            ZScoreDS = KNHS.GetZScoreValues(Convert.ToInt32(Session["PatientId"]), Convert.ToString(Session["PatientSex"]), txtHeight.Text);

            //////weight for Age//////////
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) < 15)
            {
                if (ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                    M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                    S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                    if (txtWeight.Text != "")
                        weight = Convert.ToDouble(txtWeight.Text);
                    else
                        weight = 0;

                    //Weight for age calculation
                    if (L != 0)
                        WeightAgeZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
                    else
                        WeightAgeZ = (Math.Log(weight / M)) / S;


                    if (WeightAgeZ >= 4)
                    {
                        lblWA.Text = "4";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 3 && WeightAgeZ < 4)
                    {
                        lblWA.Text = "3";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 2 && WeightAgeZ < 3)
                    {
                        lblWA.Text = "2";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 1 && WeightAgeZ < 2)
                    {
                        lblWA.Text = "1";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ > -1 && WeightAgeZ < 1)
                    {
                        lblWA.Text = "0";
                        lblWAClassification.Text = " Normal";
                        lblWA.ForeColor = Color.Green;
                        lblWAClassification.ForeColor = Color.Green;
                    }
                    else if (WeightAgeZ <= -1 && WeightAgeZ > -2)
                    {
                        lblWA.Text = "-1";
                        lblWAClassification.Text = " Mild";
                        lblWA.ForeColor = Color.Orange;
                        lblWAClassification.ForeColor = Color.Orange;
                    }
                    else if (WeightAgeZ <= -2 && WeightAgeZ > -3)
                    {
                        lblWA.Text = "-2";
                        lblWAClassification.Text = " Moderate";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ <= -3 && WeightAgeZ > -4)
                    {
                        lblWA.Text = "-3";
                        lblWAClassification.Text = " Severe";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ <= -4)
                    {
                        lblWA.Text = "-4";
                        lblWAClassification.Text = " Severe";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }

                }
                else
                {
                    lblWAClassification.Text = "Out of range";
                }
            }
            else
            {
                lblWA.Text = "";
                lblWAClassification.Text = "";
            }
            /////////////////////////////////

            ///////Weight for height calculation//////////////////////////////
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) < 15)
            {
                if (Convert.ToDouble(txtHeight.Text) <= 120 && Convert.ToDouble(txtHeight.Text) >= 45)
                {
                    try
                    {

                        if (ZScoreDS.Tables[1].Rows.Count > 0)
                        {
                            L = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
                            M = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
                            S = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
                            if (txtWeight.Text != "")
                                weight = Convert.ToDouble(txtWeight.Text);
                            else
                                weight = 0;

                            if (L != 0)
                                WeightHeightZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
                            else
                                WeightHeightZ = (Math.Log(weight / M)) / S;

                            //lblWH.Text = string.Format("{0:f2}", WeightHeightZ);// WeightHeightZ.ToString("##.##");

                        }
                        else
                        {
                            lblWH.Text = "Out of range";
                        }

                        if (WeightHeightZ >= 4)
                        {
                            lblWH.Text = "4";
                            lblWHClassification.Text = " Overweight";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ >= 3 && WeightHeightZ < 4)
                        {
                            lblWH.Text = "3";
                            lblWHClassification.Text = " Overweight";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ >= 2 && WeightHeightZ < 3)
                        {
                            lblWH.Text = "2";
                            lblWHClassification.Text = " Overweight";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ >= 1 && WeightHeightZ < 2)
                        {
                            lblWH.Text = "1";
                            lblWHClassification.Text = " Overweight";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ > -1 && WeightHeightZ < 1)
                        {
                            lblWH.Text = "0";
                            lblWHClassification.Text = " Normal";
                            lblWH.ForeColor = Color.Green;
                            lblWHClassification.ForeColor = Color.Green;
                        }
                        else if (WeightHeightZ <= -1 && WeightHeightZ > -2)
                        {
                            lblWH.Text = "-1";
                            lblWHClassification.Text = " Mild";
                            lblWH.ForeColor = Color.Orange;
                            lblWHClassification.ForeColor = Color.Orange;
                        }
                        else if (WeightHeightZ <= -2 && WeightHeightZ > -3)
                        {
                            lblWH.Text = "-2";
                            lblWHClassification.Text = " Moderate";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ <= -3 && WeightHeightZ > -4)
                        {
                            lblWH.Text = "-3";
                            lblWHClassification.Text = " Severe";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                        else if (WeightHeightZ <= -4)
                        {
                            lblWH.Text = "-4";
                            lblWHClassification.Text = " Severe";
                            lblWH.ForeColor = Color.Red;
                            lblWHClassification.ForeColor = Color.Red;
                        }
                    }

                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    lblWH.Text = "";
                    lblWHClassification.Text = "";
                }
            }
            else
            {
                lblWH.Text = "";
                lblWHClassification.Text = "";
            }

            //////////////////////////////////////////////////////////////////////

            ////BMIz (Z-Score Calculation)////////////////////////////
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                if (ZScoreDS.Tables[2].Rows.Count > 0)
                {
                    L = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
                    M = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
                    S = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
                    if (txtHeight.Text != "" && txtWeight.Text != "")
                        bmi = Convert.ToDouble(txtWeight.Text) / ((Convert.ToDouble(txtHeight.Text) / 100) * (Convert.ToDouble(txtHeight.Text) / 100));
                    else
                        bmi = 0;

                    if (L != 0)
                        BMIz = ((Math.Pow((bmi / M), L)) - 1) / (S * L);
                    else
                        BMIz = (Math.Log(bmi / M)) / S;

                    lblBMIz.Text = string.Format("{0:f2}", BMIz);

                    if (BMIz >= 4)
                    {
                        lblBMIz.Text = "4";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 3 && BMIz < 4)
                    {
                        lblBMIz.Text = "3";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 2 && BMIz < 3)
                    {
                        lblBMIz.Text = "2";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 1 && BMIz < 2)
                    {
                        lblBMIz.Text = "1";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz > -1 && BMIz < 1)
                    {
                        lblBMIz.Text = "0";
                        lblBMIzClassification.Text = " Normal";
                        lblBMIz.ForeColor = Color.Green;
                        lblBMIzClassification.ForeColor = Color.Green;
                    }
                    else if (BMIz <= -1 && BMIz > -2)
                    {
                        lblBMIz.Text = "-1";
                        lblBMIzClassification.Text = " Mild";
                        lblBMIz.ForeColor = Color.Orange;
                        lblBMIzClassification.ForeColor = Color.Orange;
                    }
                    else if (BMIz <= -2 && BMIz > -3)
                    {
                        lblBMIz.Text = "-2";
                        lblBMIzClassification.Text = " Moderate";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz <= -3 && BMIz > -4)
                    {
                        lblBMIz.Text = "-3";
                        lblBMIzClassification.Text = " Severe";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz <= -4)
                    {
                        lblBMIz.Text = "-4";
                        lblBMIzClassification.Text = " Severe";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblBMIz.Text = "";
                    lblBMIzClassification.Text = "";
                }

            }
            else
            {
                lblBMIz.Text = "";
                lblBMIzClassification.Text = "";
            }
            /////////////////////////////////////////////////////////

            ///////Height for age calculation/////////////////////////////
            //if (L != 0)
            //    HAz = ((Math.Pow((heightInCm / M), L)) - 1) / (S * L);
            //else
            //    HAz = (Math.Log(heightInCm / M)) / S;

            /////////////////////////////////////////////////////////////

            
            

            

            ZScoreDS.Dispose();

        }

        protected void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                if (txtHeight.Text != "" && txtWeight.Text != "")
                {
                    calculateZScores();
                }
                else if (txtHeight.Text == "")
                {
                    lblWHClassification.Text = "Enter Height";
                    lblWHClassification.ForeColor = Color.Red;
                }
                else if (txtWeight.Text == "")
                {
                    lblWAClassification.Text = "Enter Weight";
                    lblWHClassification.Text = "Enter weight";
                    lblWAClassification.ForeColor = Color.Red;
                    lblWHClassification.ForeColor = Color.Red;
                }
            }

        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                if (txtHeight.Text != "" && txtWeight.Text != "")
                {
                    calculateZScores();
                }
                else if (txtHeight.Text == "")
                {
                    lblWHClassification.Text = "Enter Height";
                    lblWHClassification.ForeColor = Color.Red;
                }
                else if (txtWeight.Text == "")
                {
                    lblWAClassification.Text = "Enter Weight";
                    lblWHClassification.Text = "Enter weight";
                    lblWAClassification.ForeColor = Color.Red;
                    lblWHClassification.ForeColor = Color.Red;
                }
            }

        }

        
    }
}