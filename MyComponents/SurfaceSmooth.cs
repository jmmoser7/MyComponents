using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyComponents
{
    public class SurfaceSmooth : GH_Component
    {

        public SurfaceSmooth()
          : base
            (
                "SurfaceSmooth",
                "Smooth",
                "Smooths out areas of higere curviture on a NURBS surface",
                "Tools", 
                "Surface"
             )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddSurfaceParameter("surface", "srf", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("strength", "st", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("iterations", "it", "", GH_ParamAccess.item);
            pManager.AddBooleanParameter("fixed", "f", "", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddSurfaceParameter("smoothSurface", "s", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Surface s1 = null; DA.GetData("surface", ref s1); s1.ToNurbsSurface();

            //NurbsSurface s2 = null; s2 = s1;
            double st = new double(); DA.GetData("strength", ref st);
            double it = new double(); DA.GetData("iterations", ref it);
            SmoothingCoordinateSystem pl = new SmoothingCoordinateSystem();
            Surface s2 = null;
            bool t = new bool(); DA.GetData("fixed", ref t);



            for (int i = 0; i < it; i++)
            {
                s2 = s1.Smooth(st, false, false, true, t, pl);
               // s1.Smooth(st, true, true, true)
                s1 = s2;
            }
            DA.SetData("smoothSurface", s1);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("9ecfa147-ca49-4cb4-a278-d537437534fa"); }
        }
    }
}