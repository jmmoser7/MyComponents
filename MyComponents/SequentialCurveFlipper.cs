using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace MyComponents
{
    public class SequentialCurveFlipper : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SequentialCurveFlipper()
          : base(
                "SequentialCurveFlipper", 
                "Flippy",
                "a node that marches through a list and sequentially flips curves to point in the same direction as the one tha preceded them.",
                "Tools", 
                "Curves"
                )
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("flipedCurve", "Fc", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Curve> iCurves = new List<Curve>();
            List<Curve> flipedCrv = new List<Curve>();

            DA.GetDataList("curve", iCurves);

            for (int i = 0; i < iCurves.Count; i++)
            {
                if (i == 0)
                {
                    flipedCrv.Add(iCurves[i]);
                }
                else
                {
                    Point3d p1 = new Point3d(iCurves[i].PointAtStart);
                    Point3d p2 = new Point3d(iCurves[i].PointAtEnd);
                    Point3d p3 = new Point3d(iCurves[i-1].PointAtStart);
                    Point3d p4 = new Point3d(iCurves[i-1].PointAtEnd);

                    Vector3d v1 = new Vector3d(p2 - p1);
                    Vector3d v2 = new Vector3d(p4 - p3);

                    v1.Unitize();
                    v2.Unitize();

                    Vector3d v3 = new Vector3d(v1 + v2);
                    double length = v3.Length;

                    if(length < 1.414)
                    {
                        iCurves[i].Reverse();
                        flipedCrv.Add(iCurves[i]);
                    }
                    else
                    {
                        flipedCrv.Add(iCurves[i]);
                    }
                }
            }
            DA.SetDataList("flipedCurve", flipedCrv);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return MyComponents.Properties.Resources.CurveFlipper;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b5f9ff5c-39d3-469c-8083-f716c16d3cca"); }
        }
    }
}
