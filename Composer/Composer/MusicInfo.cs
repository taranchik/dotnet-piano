using System;

namespace Composer
{
    public enum Tempo
    {
        Presto = 5,
        Allegro = 10,
        Moderato = 12,
        Andante = 14,
        Adagio = 15,
        Lento = 16,
        Largo = 18,
        Grave = 20
    }

    static class MusicInfo
    {
        public static readonly Tuple<string, int, bool>[] NoteInfo = new Tuple<string, int, bool>[] // Note Name, Staff Position, Is Semitone
        {
            // First Octave
            new Tuple<string, int, bool>("C0", 0, false),
            new Tuple<string, int, bool>("C#0", 0, true),
            new Tuple<string, int, bool>("D0", 1, false),
            new Tuple<string, int, bool>("D#", 1, true),
            new Tuple<string, int, bool>("E0", 2, false),
            new Tuple<string, int, bool>("F0", 3, false),
            new Tuple<string, int, bool>("F#0", 3, true),
            new Tuple<string, int, bool>("G0", 4, false),
            new Tuple<string, int, bool>("G#0", 4, true),
            new Tuple<string, int, bool>("A0", 5, false),
            new Tuple<string, int, bool>("A#0", 5, true),
            new Tuple<string, int, bool>("B0", 6, false),
            // Second Octave
            new Tuple<string, int, bool>("C1", 7, false),
            new Tuple<string, int, bool>("C#1", 7, true),
            new Tuple<string, int, bool>("D1", 8, false),
            new Tuple<string, int, bool>("D#1", 8, true),
            new Tuple<string, int, bool>("E1", 9, false),
            new Tuple<string, int, bool>("F1", 10, false),
            new Tuple<string, int, bool>("F#1", 10, true),
            new Tuple<string, int, bool>("G1", 11, false),
            new Tuple<string, int, bool>("G#1", 11, true),
            new Tuple<string, int, bool>("A1", 12, false),
            new Tuple<string, int, bool>("A#1", 12, true),
            new Tuple<string, int, bool>("B", 13, false),
        };

        public static readonly int[] DurationScale = new int[]
        {
            1, // Semiquaver
            2, // Quaver
            4, // Crotchet
            8, // Minim
            13, // Dotted Minim
            16  // Semibreve
        };
    }
}
