using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyComponents
{
    public class Get_Z : GH_Component
    {

        public Get_Z()
          : base
            (
                "Get_Z", 
                "zzz",
                "Gets the z-vector of a plane",
                "Tools", 
                "Planes"
            )
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("plane", "pl", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("Z-Vector", "z", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Plane pl = new Plane();
            Vector3d v = new Vector3d();

            DA.GetData("plane", ref pl);
            v = pl.ZAxis; v.Unitize();
            DA.SetData("Z-Vector",  v);
        }


        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return MyComponents.Properties.Resources.PlaneZ;
            }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("91f419be-fea2-44fa-bfc3-ad9ba6e13388"); }
        }
    }
}