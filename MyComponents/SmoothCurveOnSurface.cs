using System;
using System.Collections.Generic;
using System.Diagnostics;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Geometry.Collections;

namespace MyComponents
{
    public class SmoothCurveOnSurface : GH_Component
    {

        public SmoothCurveOnSurface()
          : base
            (
                "SmoothCurveOnSurface", 
                "sc",
                "Description",
                "Tools", 
                "Curves"
            )
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);
            pManager.AddSurfaceParameter("srf", "s", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("strength", "st", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("iterations", "i", "", GH_ParamAccess.item);
            //pManager.AddNumberParameter("range", "r", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "cv", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve cv1 = null; DA.GetData("curve", ref cv1);
            Curve cv2 = null;
            Curve cv3 = null;
            Surface srf = null; DA.GetData("srf", ref srf);  //srf.ToBrep(); 
            Brep b = null; b = srf.ToBrep();
            BrepFace bf = null; bf = b.Faces[0];
            Curve[] cvs = new Curve[0];
            
            double s = new double(); 
            if((DA.GetData("strength", ref s)) == false)
            {
                s = 0.05;
            }
            double it = new double(); DA.GetData("iterations", ref it);
           // double r = new double(); DA.GetData("range", ref r);
            SmoothingCoordinateSystem sm = new SmoothingCoordinateSystem();

            

            for(int i = 0; i < it; i++)
            {
                //int num = new int(); num = cv1.SpanCount;
                cv2 = cv1.Smooth(s, true, true, true, true, sm);
                //cv2 = cv1;
                cvs = Curve.PullToBrepFace(cv2, bf, 0.01);                
                cv1 = cvs[0];
                cv1 = cv1.Fit(3, 0.1, 0.0);

            }
            DA.SetData("curve", cv1);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return MyComponents.Properties.Resources.SmoothCurveOnSrf;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("7a7843dc-2967-42ac-9bd5-f76818975167"); }
        }
    }
}