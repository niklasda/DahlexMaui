
namespace DahlexApp.Logic.Models;

    //[DataContract]
    public class GameSettings
    {
        public GameSettings(IntSize canvasSize)
        {
            _canvasSize = canvasSize;
        }

        private readonly IntSize _canvasSize;

        //   [DataMember]
        public string PlayerName { get; set; } = "Dr. Who";

        //   [DataMember]
        public bool LessSound { get; set; }

        /// <summary>
        /// Number of squares on the board
        /// </summary>
        //   [IgnoreDataMember]
        public IntSize BoardSize
        {
            get
            {
                int h = (_canvasSize.Height / SquareSize.Height);
                int w = (_canvasSize.Width / SquareSize.Width);
                return new IntSize(w, h);
            }
        }

        // w, h

        /// <summary>
        /// The size of the squares on the board, TODO with or without margin ???
        /// </summary>
     //   [IgnoreDataMember]
        public readonly IntSize SquareSize = new IntSize(37, 37); // image size 42 x 42

        /// <summary>
        /// The offset to apply to get the images inside the squares
        /// </summary>
    //    [IgnoreDataMember]
        public readonly IntPoint ImageOffset = new IntPoint(1, 1); // w, h

        /// <summary>
        /// The distance between squares
        /// </summary>
    //    [IgnoreDataMember]
        public readonly IntPoint LineWidth = new IntPoint(0, 0);

        //[IgnoreDataMember]
        //public bool IsFirstRun;
        //    [IgnoreDataMember]
        public int MaxNumberOfLevel
        {
            get { return (BoardSize.Width * BoardSize.Height) / 4 + 10; }
        }
    }
