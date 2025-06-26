using System;

namespace Cube.Model;

public enum Face : byte
{
    /// <summary>Upper</summary>
    U,
    /// <summary>Right</summary>
    R,
    /// <summary>Front</summary>
    F,
    /// <summary>Down</summary>
    D,
    /// <summary>Left</summary>
    L,
    /// <summary>Back</summary>
    B,
}

// todo match the order above
public enum Color : byte
{
    White,
    Blue,
    Red,
    Yellow,
    Green,
    Orange,
}

// this is a sticker on the cube, each one with color. There are 54 facelets on a Rubik's Cube.
public enum Facelet : byte
{
    /// <summary>Upper Back Left</summary>
    UBL,
    /// <summary>Upper Back</summary>
    UB,
    /// <summary>Upper Back Right</summary>
    UBR,
    /// <summary>Upper Left</summary>
    UL,
    /// <summary>Upper</summary>
    U,
    /// <summary>Upper Right</summary>
    UR,
    /// <summary>Upper Front Left</summary>
    UFL,
    /// <summary>Upper Front</summary>
    UF,
    /// <summary>Upper Front Right</summary>
    UFR,

    /// <summary>Right Upper Back</summary>
    RUB,
    /// <summary>Right Upper</summary>
    RU,
    /// <summary>Right Upper Front</summary>
    RUF,
    /// <summary>Right Back</summary>
    RB,
    /// <summary>Right</summary>
    R,
    /// <summary>Right Front</summary>
    RF,
    /// <summary>Right Down Back</summary>
    RDB,
    /// <summary>Right Down</summary>
    RD,
    /// <summary>Right Down Front</summary>
    RDF,

    /// <summary>Front Upper Left</summary>
    FLU,
    /// <summary>Front Upper</summary>
    FU,
    /// <summary>Front Upper Right</summary>
    FUR,
    /// <summary>Front Left</summary>
    FL,
    /// <summary>Front</summary>
    F,
    /// <summary>Front Right</summary>
    FR,
    /// <summary>Front Lower Left</summary>
    FLD,
    /// <summary>Front Down</summary>
    FD,
    /// <summary>Front Lower Right</summary>
    FRD,

    /// <summary>Down Front Left</summary>
    DFL,
    /// <summary>Down Front</summary>
    DF,
    /// <summary>Down Front Right</summary>
    DFR,
    /// <summary>Down Left</summary>
    DL,
    /// <summary>Down</summary>
    D,
    /// <summary>Down Right</summary>
    DR,
    /// <summary>Down Back Left</summary>
    DBL,
    /// <summary>Down Back</summary>
    DB,
    /// <summary>Down Back Right</summary>
    DBR,

    /// <summary>Left Upper Back</summary>
    LUB,
    /// <summary>Left Upper</summary>
    LU,
    /// <summary>Left Upper Front</summary>
    LUF,
    /// <summary>Left Back</summary>
    LB,
    /// <summary>Left</summary>
    L,
    /// <summary>Left Front</summary>
    LF,
    /// <summary>Left Down Back</summary>
    LDB,
    /// <summary>Left Down</summary>
    LD,
    /// <summary>Left Down Front</summary>
    LDF,

    /// <summary>Back Upper Right</summary>
    BUR,
    /// <summary>Back Upper</summary>
    BU,
    /// <summary>Back Upper Left</summary>
    BUL,
    /// <summary>Back Right</summary>
    BR,
    /// <summary>Back</summary>
    B,
    /// <summary>Back Left</summary>
    BL,
    /// <summary>Back Down Right</summary>
    BDR,
    /// <summary>Back Down</summary>
    BD,
    /// <summary>Back Down Left</summary>
    BDL,
}

// this is a cubie on the cube, each one with two or three stickers. There are 8 corners and 12 edges.
public enum Cubelet : byte
{
    /// <summary>Upper Back Left</summary>
    UBL,
    /// <summary>Upper Back</summary>
    UB,
    /// <summary>Upper Back Right</summary>
    UBR,
    /// <summary>Upper Right</summary>
    UR,
    /// <summary>Upper Front Right</summary>
    UFR,
    /// <summary>Upper Front</summary>
    UF,
    /// <summary>Upper Front Left</summary>
    UFL,
    /// <summary>Upper Left</summary>
    UL,

    /// <summary>Back Left</summary>
    BL,
    /// <summary>Back Right</summary>
    BR,
    /// <summary>Front Right</summary>
    FR,
    /// <summary>Front Left</summary>
    FL,

    /// <summary>Down Back Left</summary>
    DBL,
    /// <summary>Down Back</summary>
    DB,
    /// <summary>Down Back Right</summary>
    DBR,
    /// <summary>Down Right</summary>
    DR,
    /// <summary>Down Front Right</summary>
    DFR,
    /// <summary>Down Front</summary>
    DF,
    /// <summary>Down Front Left</summary>
    DFL,
    /// <summary>Down Left</summary>
    DL,
}

public enum CornerCubelet : byte
{
    /// <summary>Upper Back Left</summary>
    UBL,
    /// <summary>Upper Back</summary>
    UBR,
    /// <summary>Upper Front Right</summary>
    UFR,
    /// <summary>Upper Front Left</summary>
    UFL,

    /// <summary>Down Back Left</summary>
    DBL,
    /// <summary>Down Back Right</summary>
    DBR,
    /// <summary>Down Front Right</summary>
    DFR,
    /// <summary>Down Front Left</summary>
    DFL,
}

public enum EdgeCubelet : byte
{
    /// <summary>Upper Back</summary>
    UB,
    /// <summary>Upper Right</summary>
    UR,
    /// <summary>Upper Front</summary>
    UF,
    /// <summary>Upper Left</summary>
    UL,

    /// <summary>Back Left</summary>
    BL,
    /// <summary>Back Right</summary>
    BR,
    /// <summary>Front Right</summary>
    FR,
    /// <summary>Front Left</summary>
    FL,

    /// <summary>Down Back</summary>
    DB,
    /// <summary>Down Right</summary>
    DR,
    /// <summary>Down Front</summary>
    DF,
    /// <summary>Down Left</summary>
    DL,
}

public static class CubeInfo
{
    public static (Face, Face) ToFace(this EdgeCubelet cubie)
    {
        return cubie switch
        {
            EdgeCubelet.UB => (Face.U, Face.B),
            EdgeCubelet.UR => (Face.U, Face.R),
            EdgeCubelet.UF => (Face.U, Face.F),
            EdgeCubelet.UL => (Face.U, Face.L),
            EdgeCubelet.BL => (Face.B, Face.L),
            EdgeCubelet.BR => (Face.B, Face.R),
            EdgeCubelet.FR => (Face.F, Face.R),
            EdgeCubelet.FL => (Face.F, Face.L),
            EdgeCubelet.DB => (Face.D, Face.B),
            EdgeCubelet.DR => (Face.D, Face.R),
            EdgeCubelet.DF => (Face.D, Face.F),
            EdgeCubelet.DL => (Face.D, Face.L),
            _ => throw new InvalidOperationException(cubie.ToString()),
        };
    }

    public static (Face ud, Face bf, Face lr) ToFace(this CornerCubelet cubie)
    {
        return cubie switch
        {
            CornerCubelet.UBL => (Face.U, Face.B, Face.L),
            CornerCubelet.UBR => (Face.U, Face.B, Face.R),
            CornerCubelet.UFR => (Face.U, Face.F, Face.R),
            CornerCubelet.UFL => (Face.U, Face.F, Face.L),
            CornerCubelet.DBL => (Face.D, Face.B, Face.L),
            CornerCubelet.DBR => (Face.D, Face.B, Face.R),
            CornerCubelet.DFR => (Face.D, Face.F, Face.R),
            CornerCubelet.DFL => (Face.D, Face.F, Face.L),
            _ => throw new InvalidOperationException(cubie.ToString()),
        };
    }

    public static (Facelet, Facelet) ToFacelet(this EdgeCubelet cubie)
    {
        return cubie switch
        {
            EdgeCubelet.UB => (Facelet.UB, Facelet.BU),
            EdgeCubelet.UR => (Facelet.UR, Facelet.RU),
            EdgeCubelet.UF => (Facelet.UF, Facelet.FU),
            EdgeCubelet.UL => (Facelet.UL, Facelet.LU),
            EdgeCubelet.BL => (Facelet.BL, Facelet.LB),
            EdgeCubelet.BR => (Facelet.BR, Facelet.RB),
            EdgeCubelet.FR => (Facelet.FR, Facelet.RF),
            EdgeCubelet.FL => (Facelet.FL, Facelet.LF),
            EdgeCubelet.DB => (Facelet.DB, Facelet.BD),
            EdgeCubelet.DR => (Facelet.DR, Facelet.RD),
            EdgeCubelet.DF => (Facelet.DF, Facelet.FD),
            EdgeCubelet.DL => (Facelet.DL, Facelet.LD),
            _ => throw new InvalidOperationException(cubie.ToString()),
        };
    }

    public static (Facelet ud, Facelet bf, Facelet lr) ToFacelet(this CornerCubelet cubie)
    {
        return cubie switch
        {
            CornerCubelet.UBL => (Facelet.UBL, Facelet.BUL, Facelet.LUB),
            CornerCubelet.UBR => (Facelet.UBR, Facelet.BUR, Facelet.RUB),
            CornerCubelet.UFR => (Facelet.UFR, Facelet.FUR, Facelet.RUF),
            CornerCubelet.UFL => (Facelet.UFL, Facelet.FLU, Facelet.LUF),
            CornerCubelet.DBL => (Facelet.DBL, Facelet.BDL, Facelet.LDB),
            CornerCubelet.DBR => (Facelet.DBR, Facelet.BDR, Facelet.RDB),
            CornerCubelet.DFR => (Facelet.DFR, Facelet.FRD, Facelet.RDF),
            CornerCubelet.DFL => (Facelet.DFL, Facelet.FLD, Facelet.LDF),
            _ => throw new InvalidOperationException(cubie.ToString()),
        };
    }

    public static bool IsCorner(this Cubelet cubie)
    {
        return !IsEdge(cubie);
    }

    public static bool IsCorner(this Facelet facelet)
    {
        return !IsEdge(facelet);
    }

    public static bool IsEdge(this Cubelet cubie)
    {
        return cubie switch
        {
            Cubelet.UB or Cubelet.UR or Cubelet.UF or Cubelet.UL or Cubelet.BL or Cubelet.BR or Cubelet.FR or Cubelet.FL or Cubelet.DB or Cubelet.DR or Cubelet.DF or Cubelet.DL => false,
            _ => true,
        };
    }

    public static bool IsEdge(this Facelet facelet)
    {
        return facelet switch
        {
            Facelet.UB or Facelet.UR or Facelet.UF or Facelet.UL or Facelet.BL or Facelet.BR or Facelet.FR or Facelet.FL or Facelet.DB or Facelet.DR or Facelet.DF or Facelet.DL
            or Facelet.BU or Facelet.RU or Facelet.FU or Facelet.LU or Facelet.LB or Facelet.RB or Facelet.RF or Facelet.LF or Facelet.BD or Facelet.RD or Facelet.FD or Facelet.LD => true,
            _ => false,
        };
    }
}