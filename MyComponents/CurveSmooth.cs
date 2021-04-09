using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyComponents
{
    public class CurveSmooth : GH_Component
    {

        public CurveSmooth()
          : base
            (
                "CurveSmooth", 
                "cs",
                "smoothe a curve",
                "Tools", 
                "Curves"
             )
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("factor", "f", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("iterations", "i", "", GH_ParamAccess.item);
           /* pManager.AddBooleanParameter("smooth-X", "x", "", GH_ParamAccess.item);
            pManager.AddBooleanParameter("smooth-y", "y", "", GH_ParamAccess.item);
            pManager.AddBooleanParameter("smooth-z", "z", "", GH_ParamAccess.item);
           pManager.AddPlaneParameter("referencePlane", "rp", "", GH_ParamAccess.item);*/
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve cv = null;
            Curve cv1 = null;
            double sm = new double();
            double it = new double();
            /*  bool x = new bool(); x = true;
              bool y = new bool(); y = true;
              bool z = new bool(); z = true;
             // Plane pl = new Plane(); pl = Plane.WorldXY;*/


            DA.GetData("curve", ref cv);
            DA.GetData("factor", ref sm);
            DA.GetData("iterations", ref it);
            /* DA.GetData("smooth-X", ref x);
             DA.GetData("smooth-Y", ref y);
             DA.GetData("smooth-Z", ref z);
             DA.GetData("referencePlane", ref pl);  */
            SmoothingCoordinateSystem pl = new SmoothingCoordinateSystem(); 
            
            for(int i = 0; i<it; i++)
            {
                cv1 = cv.Smooth(sm, true, true, true, true, pl);
                cv = cv1;
            }
            

            DA.SetData("curve", cv1);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return MyComponents.Properties.Resources.curveSmoothing;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("77a0a293-f7b3-4ac0-aafd-a748804eb853"); }
        }
    }
}