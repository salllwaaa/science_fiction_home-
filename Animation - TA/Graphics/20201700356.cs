using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;

        uint cuboidBufferID;
        uint cuboidTexturedBufferID;
        uint cylinderBuffer1ID;
        uint cylinderBuffer2ID;
        uint cylinderBuffer3ID;
        uint cylinderBuffer4ID;
        uint triagnleBufferID;
        //uint indeciesBufferID;
        uint xyzAxesBufferID;
        uint polygonsID;
        uint lineloopid;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;

        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const int CIRCLE_EDGES = 10;


        public float translationX = 0,
                     translationY = 0,
                     translationZ = 0;

        Stopwatch timer = Stopwatch.StartNew();
        vec3 opjectcenter;
        Texture qubidtex;
        Texture floortex;
        public void Initialize()
        {
            timer.Start();
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\20201700356.fragmentshader");
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1);

            qubidtex = new Texture(projectPath + "\\Textures\\wood.jpg", 1);
            floortex = new Texture(projectPath + "\\Textures\\Garden.jpg", 1);

            float[] triangles = {
		        //4th triangles 



                  //face two 
                   20.0f,  30.0f, 30.0f, 1.0f, 0.0f, 0.0f,//3
                   15.0f,  20.0f, 40.0f, 0.0f, 1.0f, 0.0f,//top
                   10.0f,  30.0f, 30.0f,0.0f, 0.0f, 1.0f,//4
                  //face four
                15.0f,  20.0f, 40.0f,1.0f, 0.0f, 0.0f,//top
                 20.0f,  30.0f, 30.0f,0.0f, 1.0f, 0.0f,//3
                20.0f,  10.0f, 30.0f,0.0f, 0.0f, 1.0f,//1


                 //face one 
                15.0f,  20.0f, 40.0f, 1.0f, 0.0f, .0f,//top
                20.0f,  10.0f, 30.0f, 0.0f, 1.0f, 1.0f,//1
                10.0f,  10.0f, 30.0f, 0.0f, 0.0f, 1.0f,//2

                 //face three
                10.0f,  30.0f, 30.0f,1.0f, 0.0f, 0.0f,//4
                10.0f,  10.0f, 30.0f, 0.0f, 1.0f, 0.0f,//2
                15.0f,  20.0f, 40.0f, 0.0f, 0.0f, 1.0f,//top



            };

            float[] cuboidVerts = {

                    //1st cubid
          //      //Bottom Base
		        0.0f,  0.0f, 15.0f, 1.0f, 0.0f, 0.0f,
                30.0f,  0.0f, 15.0f,0.0f, 1.0f, 0.0f,
                30.0f,  40.0f, 15.0f,0.0f, 0.0f, 1.0f,
                0.0f,  40.0f, 15.0f, 1.0f, 0.0f, 0.0f,


                0.0f,  0.0f, 15.0f, 1.0f, 0.0f, 0.0f,
                0.0f,  0.0f, 20.0f, 0.0f, 1.0f, 0.0f,
                0.0f,  40.0f, 20.0f, 0.0f, 0.0f, 1.0f,
                0.0f,  40.0f, 15.0f, 1.0f, 0.0f, 0.0f,


                0.0f,  40.0f, 15.0f, 1.0f, 0.0f, 0.0f,
                0.0f,  40.0f, 20.0f, 0.0f, 1.0f, 0.0f,
                30.0f,  40.0f, 20.0f, 0.0f, 0.0f, 1.0f,
                30.0f,  40.0f, 15.0f, 1.0f, 0.0f, 0.0f,


                30.0f,  40.0f, 15.0f, 1.0f, 0.0f, 0.0f,
                30.0f,  40.0f, 20.0f, 0.0f, 1.0f, 0.0f,
                30.0f,  0.0f, 20.0f,0.0f, 0.0f, 1.0f,
                30.0f,  0.0f, 15.0f, 1.0f, 0.0f, 0.0f,



                 0.0f,  0.0f, 15.0f, 1.0f, 0.0f, 0.0f,
                0.0f,  0.0f, 20.0f, 0.0f, 1.0f, 0.0f,
                30.0f,  0.0f, 20.0f,0.0f, 0.0f, 1.0f,
                30.0f,  0.0f, 15.0f, 1.0f, 0.0f, 0.0f,

                //Top Base
                0.0f,  0.0f, 20.0f, 1.0f, 0.0f, 0.0f,
                30.0f,  0.0f, 20.0f, 0.0f, 1.0f, 0.0f,
                30.0f,  40.0f, 20.0f, 0.0f, 0.0f, 1.0f,
                0.0f,  40.0f, 20.0f, 1.0f, 0.0f, 0.0f,


                 //3d cubid

                    //Bottom Base
		        0.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                30.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                30.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                0.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,


               0.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                30.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                20.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                10.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,

                0.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                10.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                10.0f,  30.0f, 30.0f, 1.0f, 0.0f, 0.0f,
                0.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,

               0.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,
               10.0f,  30.0f, 30.0f, 1.0f, 0.0f, 0.0f,
                20.0f,  30.0f, 30.0f, 1.0f, 0.0f, 0.0f,
               30.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,

                30.0f,  40.0f, 20.0f, 1.0f, 0.0f, 1.0f,
                20.0f,  30.0f, 30.0f, 1.0f, 0.0f, 0.0f,
                20.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                30.0f,  0.0f, 20.0f, 1.0f, 0.0f, 1.0f,


                 //Top Base
                10.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                20.0f,  10.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                20.0f,  30.0f, 30.0f, 1.0f, 0.0f, 1.0f,
                10.0f,  30.0f, 30.0f, 1.0f, 0.0f, 1.0f,

            };

            float[] cuboidtexturedVerts = {
                 //2nd  cubid
                 //Bottom Base
		        -5.0f, -5.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,0,
                35.0f,  -5.0f, 5.0f, 0.0f, 0.0f, 0.0f,3,0,
                35.0f,  45.0f, 5.0f, 0.0f, 0.0f, 0.0f,3,3,
                -5.0f,  45.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,3,

                -5.0f, -5.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,0,
                35.0f,  -5.0f, 5.0f, 0.0f, 0.0f, 0.0f,3,0,
                 30.0f,  0.0f, 15.0f,0.0f, 0.0f, 0.0f,3,3,
                 0.0f,  0.0f, 15.0f, 0.0f, 0.0f, 0.0f,0,3,

                -5.0f, -5.0f, 5.0f,0.0f, 0.0f, 0.0f,0,0,
                0.0f,  0.0f, 15.0f, 0.0f, 0.0f, 0.0f,3,0,
                0.0f,  40.0f, 15.0f, 0.0f, 0.0f, 0.0f,3,3,
                -5.0f,  45.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,3,

                -5.0f,  45.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,0,
                0.0f,  40.0f, 15.0f,  0.0f, 0.0f, 0.0f,3,0,
                30.0f,  40.0f, 15.0f, 0.0f, 0.0f, 0.0f,3,3,
                35.0f,  45.0f, 5.0f, 0.0f, 0.0f, 0.0f,0,3,

                 35.0f,  45.0f, 5.0f,0.0f, 0.0f, 0.0f,0,0,
                30.0f,  40.0f, 15.0f, 0.0f, 0.0f, 0.0f,3,0,
                30.0f,  0.0f, 15.0f,0.0f, 0.0f, 0.0f,3,3,
                35.0f,  -5.0f, 5.0f,0.0f, 0.0f, 0.0f,0,3,


                //Top Base
                0.0f,  0.0f, 15.0f,  0.0f, 0.0f, 0.0f,0,0,
               30.0f,  0.0f, 15.0f,  0.0f, 0.0f, 0.0f,0,0,
                30.0f,  40.0f, 15.0f,  0.0f, 0.0f, 0.0f,0,0,
               0.0f,  40.0f, 15.0f, 0.0f, 0.0f, 0.0f,0,0,
            };

            triagnleBufferID = GPU.GenerateBuffer(triangles);

            cuboidTexturedBufferID = GPU.GenerateBuffer(cuboidtexturedVerts);




            float[] xyzAxesVertices = {
		        //x
		        0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        100.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        //y
	            0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		        0.0f, 100.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		        //z
	            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f,  //B
		        0.0f, 0.0f, 100.0f, 0.0f, 0.0f, 1.0f,  //B
            };


            float[] lineloop = {


                //-25.0f, -25.0f, -3.0f, 0.0f,0.0f, 0.0f,
                //50.0f,  -25.0f, -3.0f, 0.0f, 0.0f,0.0f,
                //50.0f,  60.0f, -3.0f, 0.0f, 0.0f, 0.0f,
                //-25.0f,  60.0f, -3.0f, 0.0f, 0.0f, 0.0f,



            };


            float[] decorations = {


                 -15.0f, -15.0f, -3.0f, 0.0f,0.0f, 0.0f,0,0,
                45.0f,  -15.0f, -3.0f, 0.0f, 0.0f,0.0f,1,0,
                45.0f,  55.0f, -3.0f, 0.0f, 0.0f, 0.0f,1,1,
                -15.0f,  55.0f, -3.0f, 0.0f, 0.0f, 0.0f,0,1,


            };


            opjectcenter = new vec3(15, 20, 20);


            var cylinderPoints1 = draw_cylinder1_side();
            var cylinderPoints2 = draw_cylinder2_side();
            var cylinderPoints3 = draw_cylinder3_side();
            var cylinderPoints4 = draw_cylinder4_side();

            cuboidBufferID = GPU.GenerateBuffer(cuboidVerts);
            cylinderBuffer1ID = GPU.GenerateBuffer(cylinderPoints1);
            cylinderBuffer2ID = GPU.GenerateBuffer(cylinderPoints2);
            cylinderBuffer3ID = GPU.GenerateBuffer(cylinderPoints3);
            cylinderBuffer4ID = GPU.GenerateBuffer(cylinderPoints4);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);
            polygonsID = GPU.GenerateBuffer(decorations);
            lineloopid = GPU.GenerateBuffer(lineloop);
            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(-50, -50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 0, 1)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);

            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);


            #region XYZ axis
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion



            #region decoration
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, polygonsID);


            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            floortex.Bind();
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion


            #region lineloop
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, lineloopid);


            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));


            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());


            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 0, 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion






            #region cylinder1
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cylinderBuffer1ID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_QUAD_STRIP, 0, (CIRCLE_EDGES * 2) + 2);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion






            #region cylinder2
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cylinderBuffer2ID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_QUAD_STRIP, 0, (CIRCLE_EDGES * 2) + 2);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion



            #region cylinder3
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cylinderBuffer3ID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_QUAD_STRIP, 0, (CIRCLE_EDGES * 2) + 2);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion







            #region cylinder4
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cylinderBuffer4ID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_QUAD_STRIP, 0, (CIRCLE_EDGES * 2) + 2);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion


            #region Cuboidtextured
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cuboidTexturedBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));


            qubidtex.Bind();
            Gl.glDrawArrays(Gl.GL_QUADS, 0, 6 * 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);
            #endregion








            #region Cuboid
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cuboidBufferID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_QUADS, 0, 6 * 4 * 2);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion





            #region triangles
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, triagnleBufferID);



            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 12);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion



        }
        public float[] draw_cylinder1_side()
        {
            float centerX = -4, centerY = 44, centerZ = -3, radius = 1, height = 8;
            float R = 1.0f, G = 0.0f, B = 1.0f;
            List<float> verticies = new List<float>();

            float step = (float)(2 * Math.PI) / CIRCLE_EDGES;

            float angle = 0.0f;
            while (angle < 2 * Math.PI)
            {
                float x = centerX + (float)(radius * Math.Cos(angle));
                float y = centerY + (float)(radius * Math.Sin(angle));
                verticies.AddRange(new float[] { x, y, centerZ, R, G, B });
                verticies.AddRange(new float[] { x, y, centerZ + height, R, G, B });
                angle += step;
            }
            verticies.AddRange(verticies.GetRange(0, 12));

            return verticies.ToArray();
        }

        public float[] draw_cylinder2_side()
        {
            float centerX = -4, centerY = -4, centerZ = -3, radius = 1, height = 8;
            float R = 1.0f, G = 0.0f, B = 1.0f;
            List<float> verticies = new List<float>();

            float step = (float)(2 * Math.PI) / CIRCLE_EDGES;

            float angle = 0.0f;
            while (angle < 2 * Math.PI)
            {
                float x = centerX + (float)(radius * Math.Cos(angle));
                float y = centerY + (float)(radius * Math.Sin(angle));
                verticies.AddRange(new float[] { x, y, centerZ, R, G, B });
                verticies.AddRange(new float[] { x, y, centerZ + height, R, G, B });
                angle += step;
            }
            verticies.AddRange(verticies.GetRange(0, 12));

            return verticies.ToArray();
        }


        public float[] draw_cylinder3_side()
        {
            float centerX = 34, centerY = -4, centerZ = -3, radius = 1, height = 8;
            float R = 1.0f, G = 0.0f, B = 1.0f;
            List<float> verticies = new List<float>();

            float step = (float)(2 * Math.PI) / CIRCLE_EDGES;

            float angle = 0.0f;
            while (angle < 2 * Math.PI)
            {
                float x = centerX + (float)(radius * Math.Cos(angle));
                float y = centerY + (float)(radius * Math.Sin(angle));
                verticies.AddRange(new float[] { x, y, centerZ, R, G, B });
                verticies.AddRange(new float[] { x, y, centerZ + height, R, G, B });
                angle += step;
            }
            verticies.AddRange(verticies.GetRange(0, 12));

            return verticies.ToArray();
        }


        public float[] draw_cylinder4_side()
        {
            float centerX = 34, centerY = 44, centerZ = -3, radius = 1, height = 8;
            float R = 1.0f, G = 0.0f, B = 1.0f;
            List<float> verticies = new List<float>();

            float step = (float)(2 * Math.PI) / CIRCLE_EDGES;

            float angle = 0.0f;
            while (angle < 2 * Math.PI)
            {
                float x = centerX + (float)(radius * Math.Cos(angle));
                float y = centerY + (float)(radius * Math.Sin(angle));
                verticies.AddRange(new float[] { x, y, centerZ, R, G, B });
                verticies.AddRange(new float[] { x, y, centerZ + height, R, G, B });
                angle += step;
            }
            verticies.AddRange(verticies.GetRange(0, 12));

            return verticies.ToArray();
        }



        const float rotationSpeed = 1.0f;
        float rotationAngle = 0;
        public void Update()
        {
            timer.Stop();
            float deltatime = timer.ElapsedMilliseconds / 1000.0f;
            rotationAngle += rotationSpeed * deltatime;
            List<mat4> trans = new List<mat4>();
            trans.Add(glm.translate(new mat4(1), -1 * opjectcenter));
            trans.Add(glm.rotate(rotationAngle, new vec3(0, 0, 1)));
            trans.Add(glm.translate(new mat4(1), opjectcenter));
            trans.Add(glm.translate(new mat4(1), new vec3(translationX, 0, 0)));
            trans.Add(glm.translate(new mat4(1), new vec3(0, translationY, 0)));
            trans.Add(glm.translate(new mat4(1), new vec3(0, 0, translationZ)));
            ModelMatrix = MathHelper.MultiplyMatrices(trans);
            timer.Restart();
            timer.Start();
        }
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}