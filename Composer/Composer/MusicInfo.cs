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
        public static readonly Tuple<string, string, int, bool>[] NoteInfo = new Tuple<string, string, int, bool>[] // Resource Name, Note Name, Staff Position, Is Semitone
        {
            // First Octave
            new Tuple<string, string, int, bool>("c0", "C", 0, false),
            new Tuple<string, string, int, bool>("c_0", "C#", 0, true),
            new Tuple<string, string, int, bool>("d0", "D", 1, false),
            new Tuple<string, string, int, bool>("d_0", "D#", 1, true),
            new Tuple<string, string, int, bool>("e0", "E", 2, false),
            new Tuple<string, string, int, bool>("f0", "F", 3, false),
            new Tuple<string, string, int, bool>("f_0", "F#", 3, true),
            new Tuple<string, string, int, bool>("g0", "G", 4, false),
            new Tuple<string, string, int, bool>("g_0", "G#", 4, true),
            new Tuple<string, string, int, bool>("a0", "A", 5, false),
            new Tuple<string, string, int, bool>("a_0", "A#", 5, true),
            new Tuple<string, string, int, bool>("b0", "B", 6, false),
            // Second Octave
            new Tuple<string, string, int, bool>("c1", "C", 7, false),
            new Tuple<string, string, int, bool>("c_1", "C#", 7, true),
            new Tuple<string, string, int, bool>("d1", "D", 8, false),
            new Tuple<string, string, int, bool>("d_1", "D#", 8, true),
            new Tuple<string, string, int, bool>("e1", "E", 9, false),
            new Tuple<string, string, int, bool>("f1", "F", 10, false),
            new Tuple<string, string, int, bool>("f_1", "F#", 10, true),
            new Tuple<string, string, int, bool>("g1", "G", 11, false),
            new Tuple<string, string, int, bool>("g_1", "G#", 11, true),
            new Tuple<string, string, int, bool>("a1", "A", 12, false),
            new Tuple<string, string, int, bool>("a_1", "A#", 12, true),
            new Tuple<string, string, int, bool>("b1", "B", 13, false),
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
