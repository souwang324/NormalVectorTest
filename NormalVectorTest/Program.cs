using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace NormalVectorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NormalVectorTest1();
        }

        public static void NormalVectorTest1()
        {
            vtkSTLReader pSTLReader = vtkSTLReader.New();
            pSTLReader.SetFileName(@"..\..\..\..\res\cow.stl");
            pSTLReader.Update();

            vtkPolyDataNormals normals = vtkPolyDataNormals.New();
            normals.SetInput(pSTLReader.GetOutput());
            normals.ComputePointNormalsOn();
            normals.ComputeCellNormalsOn();
            normals.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(normals.GetOutputPort());
            //mapper.SetInput(normals.GetOutput());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();
        }

    }
}
