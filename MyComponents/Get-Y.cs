using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyComponents
{
    public class PlaneY : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PlaneY class.
        /// </summary>
        public PlaneY()
          : base(
                "Get_Y",
                "Y",
                "Gets the y-vector of a plane",
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
            pManager.AddVectorParameter("Y-Vector", "y", "", GH_ParamAccess.item);
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
            v = pl.YAxis; v.Unitize();
            DA.SetData("Y-Vector", v);
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
                return MyComponents.Properties.Resources.PlaneY;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ba5f8eb9-30c4-435e-9218-e2adbe54deb5"); }
        }
    }
}