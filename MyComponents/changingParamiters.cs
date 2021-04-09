using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasshopper.Kernel;
using Grasshopper;
using System.Drawing;

using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace MyComponents
{
    #region Methods of GH_Component interface

    public class VariableInput : GH_Component, IGH_VariableParameterComponent
    {

        public VariableInput() : base
            (
            "VariableInput", 
            "VarCalc", 
            "Component that makes some basic operations", 
            "Tools", 
            "Test"
            )
        {

        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //No input parameters, all of them will be added by the user
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Plus", "S", "Result of adding all entries", GH_ParamAccess.item);
            pManager.AddCurveParameter("oCurve", "c", "", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve cr = null;
            DA.GetData("curve", ref cr);



            
            double result = 0, aux = 0;
            for (int i = 0; i < Params.Input.Count; i++)
            {
                if (DA.GetData(i, ref aux))
                {
                    result += aux;
                }
            }
            DA.SetData(0, result);
            DA.SetData("oCurve", cr);

        }


        public override Guid ComponentGuid
        {
            get { return new Guid("831F08AB-044A-4897-A936-E30528ADA4F9"); }
        }
        #endregion

        #region Methods of IGH_VariableParameterComponent interface

        bool IGH_VariableParameterComponent.CanInsertParameter(GH_ParameterSide side, int index)
        {
            //We only let input parameters to be added (output number is fixed at one)
            if (side == GH_ParameterSide.Input)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IGH_VariableParameterComponent.CanRemoveParameter(GH_ParameterSide side, int index)
        {
            //We can only remove from the input
            if (side == GH_ParameterSide.Input && Params.Input.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        IGH_Param IGH_VariableParameterComponent.CreateParameter(GH_ParameterSide side, int index)
        {
            Param_Number param = new Param_Number();         

            param.Name = GH_ComponentParamServer.InventUniqueNickname("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Params.Input);
            param.NickName = param.Name;
            param.Description = "Param" + (Params.Input.Count + 1);
            param.SetPersistentData(0.0);

            return param;
        }



    bool IGH_VariableParameterComponent.DestroyParameter(GH_ParameterSide side, int index)
        {
            //Nothing to do here by the moment
            return true;
        }

        void IGH_VariableParameterComponent.VariableParameterMaintenance()
        {
            //Nothing to do here by the moment
        }

        #endregion

    }


}