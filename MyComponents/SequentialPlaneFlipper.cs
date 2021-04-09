using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms.VisualStyles;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Rhino.Render;

namespace MyComponents
{
    public class SequentialPlaneFlipper : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public SequentialPlaneFlipper()
          : base(
                "SequentialPlaneFlipper", 
                 "FlipMe",
                 "Automatickly aligns a list of plains by first rotating them to match the orientation of their neighbor and than aligning their x axies",
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
            pManager.AddPlaneParameter("planes", "Pi", "", GH_ParamAccess.list);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPlaneParameter("alignedPlanes", "Ap", "", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Plane> inPlanes = new List<Plane>();
            DA.GetDataList("planes", inPlanes);

            for (int i = 0; i < inPlanes.Count; i++)
            {
                Plane myPlane1 = new Plane();
                Plane myPlane2 = new Plane();
                Plane myPlane3 = new Plane();

                Vector3d vector1 = new Vector3d();
                Vector3d vector2 = new Vector3d();

                if (i == 0)
                {
                    myPlane3 = inPlanes[i];
                }
                else
                {

                    myPlane1 = inPlanes[i - 1];
                    myPlane2 = inPlanes[i];

                    Transform projection = new Transform(Transform.PlanarProjection(myPlane2));

                    vector1 = myPlane1.XAxis;
                    vector1.Transform(projection);
                    vector1.Unitize();
                    //vector1.Reverse();

                    vector2 = vector1;
                    vector2.Rotate(Math.PI / 2, myPlane2.ZAxis);
                    //vector2.Reverse();

                    Plane cPlane = new Plane(myPlane2.Origin, vector1, vector2);
                    myPlane3 = cPlane;
                }

                inPlanes.Insert(i, myPlane3);
                inPlanes.RemoveAt(i + 1);
            }

            for (int i = 0; i < inPlanes.Count; i++)
            {
                Plane mp2 = new Plane();
                Plane mp3 = new Plane();


                if (i == 0)
                {
                    mp3 = inPlanes[i];
                }
                else
                {

                    mp2 = inPlanes[i - 1];
                    mp3 = inPlanes[i];

                    double addition = (mp2.ZAxis + mp3.ZAxis).Length;
                    Boolean dispatch1 = new Boolean();
                    dispatch1 = addition < Math.Sqrt(2);

                    if (dispatch1)
                    {
                        mp3.Rotate(Math.PI, mp3.XAxis);

                    }
                    else
                    {

                    }
                }
                inPlanes.Insert(i, mp3);
                inPlanes.RemoveAt(i + 1);
            } 

            DA.SetDataList("alignedPlanes", inPlanes);
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
                return MyComponents.Properties.Resources.PlaneRotation;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8a7d8283-872d-42e7-8db6-4a29941d4095"); }
        }
    }
}