﻿using System.ComponentModel;
using Snake;

namespace Jogo
{
    class Jogo
    {
        /*
            Essa será a classe principal onde o jogo ocorre
        */
        static void Main()
        {
            // Criamos a tela: Tela(largura, altura)
            Tela tela = new Tela(100, 13);

            // Tempo em milisegundos de atualização da tela
            int tempoAtualizacao = 300;

            // Criar Cobra no centro da tela
            Cobra cobrinha = new Cobra(tela.Largura / 2, tela.Altura / 2);
            tela.InserirCobra(cobrinha);

            // Criar alimento, cobra, tela largura e tela altura devem ser passados como
            //  argumentos para evitar sobreposição
            Alimento comida = new Alimento(cobrinha, tela.Largura, tela.Altura);
            tela.InserirAlimento(comida);

            // Gerar Tela
            tela.AtualizarTela();

            // Iniciar jogo
            while (cobrinha.Viva)
            {
                // Capturamos o proximo movimento
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(false);
                    cobrinha.MudarDirecaoCobra(SnakeActions.Actions(key.Key));

                    #region Remover
                    //switch (key.Key)
                    //{
                    //    case ConsoleKey.UpArrow:
                    //        cobrinha.MudarDirecaoCobra(Direction.Top); // cima
                    //        break;
                    //    case ConsoleKey.RightArrow:
                    //        cobrinha.MudarDirecaoCobra(Direction.Right); // direita
                    //        break;
                    //    case ConsoleKey.DownArrow:
                    //        cobrinha.MudarDirecaoCobra(Direction.Down); // baixo
                    //        break;
                    //    case ConsoleKey.LeftArrow:
                    //        cobrinha.MudarDirecaoCobra(Direction.Left); // esquerda
                    //        break;
                    //    default:
                    //        break;
                    //}

                    #endregion
                }

                // Andar com a Cobra
                tela.RemoverCobra(cobrinha);
                cobrinha.Andar();
                tela.InserirCobra(cobrinha);

                // TODO: separar a parte de checar sobreposições em um método
                // Checar sobreposições da cabeça com
                //   com paredes
                if (cobrinha.PosicaoX == 0 ||
                    cobrinha.PosicaoX == tela.Largura - 1 ||
                    cobrinha.PosicaoY == 0 ||
                    cobrinha.PosicaoY == tela.Altura - 1)
                {
                    cobrinha.Viva = false;
                }
                //   com alimento
                if (cobrinha.PosicaoX == comida.PosicaoX &&
                    cobrinha.PosicaoY == comida.PosicaoY)
                {
                    cobrinha.Alimentar(comida);
                    comida = new Alimento(cobrinha, tela.Largura, tela.Altura);
                    tela.InserirAlimento(comida);
                }
                //   com o corpo
                if (cobrinha.Corpo.FirstOrDefault(c => c.PosicaoX == cobrinha.PosicaoX &&
                                                 c.PosicaoY == cobrinha.PosicaoY, null) != null)
                {
                    cobrinha.Viva = false;
                }

                tela.AtualizarTela();
                System.Threading.Thread.Sleep(tempoAtualizacao);
            }
            tela.FimDeJogo();
        }
        public bool ChecarColisao()
        {
            throw new NotImplementedException();
        }
    }

    public enum Direction
    {
        Top = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        Stay = -1
    }
}