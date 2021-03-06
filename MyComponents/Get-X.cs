using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyComponents
{
    public class PlaneX : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PlaneX class.
        /// </summary>
        public PlaneX()
          : base(
                "Get_X",
                "X",
                "Gets the x-vector of a plane",
                "Tools",
                "Planes"
            )
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("plane", "pl", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("X-Vector", "x", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Plane pl = new Plane();
            Vector3d v = new Vector3d();

            DA.GetData("plane", ref pl);
            v = pl.XAxis; v.Unitize();
            DA.SetData("X-Vector", v);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return MyComponents.Properties.Resources.PlaneX;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ef25718c-c2bc-4ac1-9164-c9f90dfeeb15"); }
        }
    }
}