namespace SSG01.Data.Enums
{
	using System;

	public enum UnitSymbols
	{
        a = 0, b = 1, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
		A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }

    public enum Direction
    {
        forward = 1, right, left, backward
    }

    public enum ActionType
    {
        None = 0, Move, Attack
    }
}