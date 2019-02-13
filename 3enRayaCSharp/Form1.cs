using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3enRayaCSharp
{
    public partial class Form1 : Form
    {

        private Button[,] boton = new Button[3, 3];
        private Label jugadorActual = new Label();
        private String jugador = "O";
        private int simbolosO = 0;
        private int simbolosX = 0;
        private bool juegoFinalizado = false;

        public Form1()
        {
            InitializeComponent();

            jugadorActual.Width = 300;
            jugadorActual.Height = 100;
            jugadorActual.Top = 0;
            jugadorActual.Left = 0;
            jugadorActual.Text = "Turno para jugador: " + jugador;
            this.Controls.Add(jugadorActual);


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boton[i, j] = new Button();
                    boton[i, j].Width = 100;
                    boton[i, j].Height = 100;
                    boton[i, j].Top = 100 + (i * 100); // Modificado para la etiqueta del usuario actual
                    boton[i, j].Left = j * 100;
                    boton[i, j].Text = "";

                    boton[i, j].Click += new EventHandler(this.botonPulsado);
                    this.Controls.Add(boton[i, j]);
                }
            }
        }
        void botonPulsado(Object sender, EventArgs e)
        {
            Button botonActual = (Button)sender;

            if (usuarioTieneSimbolosLibres())
            {
                // La casilla esta libre
                if (botonActual.Text == "")
                {
                    // Si, casilla para jugador actual
                    casillaParaJugador(botonActual);
                    // Comprueba si es ganador
                    if (esGanador())
                    {
                        // No, no puede liberarla
                        MessageBox.Show("Ganador: Jugador " + jugador);
                        // Empieza el juego de nuevo
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                boton[i, j].Text = "";
                            }
                        }
                        MessageBox.Show("El juego comienza de nuevo");
                    }
                    else
                    {
                        cambiarTurno();
                    }
                }
                else
                {
                    // No
                    MessageBox.Show("Casilla ocupada");
                }
            }
            else
            {
                if (esCasillaJugadorActual(botonActual))
                {
                    // Si, solo puede liberarla
                    liberaCasilla(botonActual);
                }
                else
                {
                    // No, no puede liberarla
                    MessageBox.Show("Casilla incorrecta");
                }
            }

        }

        /* Utiles */

        private bool usuarioTieneSimbolosLibres() 
        {
            if(jugador == "O")
            {
                // Usuario O
                return simbolosO < 3;
            }
            // Usuario X
            return simbolosX < 3;
        }

        private bool esCasillaJugadorActual(Button casilla)
        {
            return casilla.Text == jugador;
        }

        private void casillaParaJugador(Button casilla)
        {
            casilla.Text = jugador;
            if (jugador == "O")
            {
                simbolosO++;
            }
            else
            {
                simbolosX++;
            }
        }

        private void liberaCasilla(Button casilla)
        {
            casilla.Text = "";
            if (jugador == "O")
            {
                simbolosO--;
            }
            else
            {
                simbolosX--;
            }
        }

        private void cambiarTurno()
        {
            if (jugador != "O")
            {
                jugador = "O";
            }
            else
            {
                jugador = "X";
            }
            jugadorActual.Text = "Turno para jugador: " + jugador;
        }

        private bool esGanador()
        {
            // Tiene tres simbolos puestos
            bool simbolos_3 = false;
            if (jugador == "O")
            {
                simbolos_3 = simbolosO == 3;
            }
            else
            {
                simbolos_3 = simbolosX == 3;
            }

            //  Comprobamos las filas, a ver si alguna esta en linea para el jugador actual
            bool[] mismaFila = new bool[3];
            bool[] mismaColumna = new bool[3];

            for (int j = 0; j < 3; j++)
            {
                mismaFila[j] = true;
                mismaColumna[j] = true;
            }

            bool filaEnLinea = false;
            bool columnaEnLinea = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mismaFila[i] = mismaFila[i] && boton[i, j].Text == jugador;
                    mismaColumna[j] = mismaColumna[j] && boton[i, j].Text == jugador;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                filaEnLinea = filaEnLinea || mismaFila[j];
                columnaEnLinea = columnaEnLinea || mismaColumna[j];
            }

            // Diagonal \
            bool diagonal1 = boton[0, 0].Text == jugador && boton[1, 1].Text == jugador && boton[2, 2].Text == jugador;
            
            // Diagonal /
            bool diagonal2 = boton[0, 2].Text == jugador && boton[1, 1].Text == jugador && boton[2, 0].Text == jugador;
            
            // Si tiene 3 simbolos puestos y esta en linea alguna fila o alguna diagonal, el jugador actual es ganador
            return simbolos_3 && (filaEnLinea || columnaEnLinea ||  diagonal1 || diagonal2);
        }

        /* Load */
        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }



}
