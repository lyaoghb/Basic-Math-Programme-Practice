using System;
using System.Collections;
using System.Collections.Generic;

public class Matrix3D {
    public float[] rawData = new float[16];//创建4x4的矩阵
    /*
    0:();  1:();  2:();  3:();
    4:();  5:();  6:();  7:();
    8:();  9:();  10:(); 11:();
    12:(); 13:(); 14:(); 15:();
    */
    public Matrix3D() {//实现构造函数
        this.identity();//初始化矩阵
    }

    public Matrix3D(float[] data) {
        this.copyForm(data);
    }

    public Matrix3D(Matrix3D mt) {
        this.copyForm(mt);
    }

    public void copyForm(float[] data) {//编写方法:将data的数据复制过来
        for (int i = 0; i < 16; i++) {
            this.rawData[i] = data[i];
        }
    }

    public void copyForm(Matrix3D data) {//编写方法:使copyForm复制功能可接受matrix3D格式
        this.copyForm(data.rawData);
    }

    public void identity() {//编写方法:使矩阵单位化/初始化
        for (int i = 0; i < 16; i++) {
            rawData[i] = 0.0f;//设置矩阵每个数的初始值为0
        }
        rawData[0] = 1.0f;
        rawData[5] = 1.0f;
        rawData[10] = 1.0f;
        rawData[15] = 1.0f;//设置对角线的值为1
    }

    public void copyColumnForm(int column,Vector3D vec) {//编写方法:复制一列
        this.rawData[column * 4 + 0] = vec.x;
        this.rawData[column * 4 + 1] = vec.y;
        this.rawData[column * 4 + 2] = vec.z;
        this.rawData[column * 4 + 3] = vec.w;
    }

    public void copyColumnTo(int column,Vector3D vec) {//方法:获取一列
        vec.x = this.rawData[column * 4 + 0];
        vec.y = this.rawData[column * 4 + 1];
        vec.z = this.rawData[column * 4 + 2];
        vec.w = this.rawData[column * 4 + 3];
    }

    public void copyRowFrom(int row,Vector3D vec) {//方法:复制一行
        this.rawData[row + 0] = vec.x;
        this.rawData[row + 1] = vec.y;
        this.rawData[row + 2] = vec.z;
        this.rawData[row + 3] = vec.w;
    }

    public void copyRowTo(int row,Vector3D vec) {//方法:获取一行
        vec.x = this.rawData[row + 0];
        vec.y = this.rawData[row + 1];
        vec.z = this.rawData[row + 2];
        vec.w = this.rawData[row + 3];
    }

    public void append(Matrix3D lhs) {//方法:求矩阵的乘
        float m111 = rawData[0];
        float m121 = rawData[4];
        float m131 = rawData[8];
        float m141 = rawData[12];

        float m112 = rawData[1];
        float m122 = rawData[5];
        float m132 = rawData[9];
        float m142 = rawData[13];

        float m113 = rawData[2];
        float m123 = rawData[6];
        float m133 = rawData[10];
        float m143 = rawData[14];

        float m114 = rawData[3];
        float m124 = rawData[7];
        float m134 = rawData[11];
        float m144 = rawData[15];

        float m211 = lhs.rawData[0];
        float m221 = lhs.rawData[4];
        float m231 = lhs.rawData[8];
        float m241 = lhs.rawData[12];

        float m212 = lhs.rawData[1];
        float m222 = lhs.rawData[5];
        float m232 = lhs.rawData[9];
        float m242 = lhs.rawData[13];

        float m213 = lhs.rawData[2];
        float m223 = lhs.rawData[6];
        float m233 = lhs.rawData[10];
        float m243 = lhs.rawData[14];

        float m214 = lhs.rawData[3];
        float m224 = lhs.rawData[7];
        float m234 = lhs.rawData[11];
        float m244 = lhs.rawData[15];

        this.rawData[0] = m111 * m211 + m112 * m221 + m113 * m231 + m114 * m241;
        this.rawData[1] = m111 * m212 + m112 * m222 + m113 * m232 + m114 * m242;
        this.rawData[2] = m111 * m213 + m112 * m223 + m113 * m233 + m114 * m243;
        this.rawData[3] = m111 * m214 + m112 * m224 + m113 * m234 + m114 * m244;

        this.rawData[0] = m121 * m211 + m122 * m221 + m123 * m231 + m124 * m241;
        this.rawData[1] = m121 * m212 + m122 * m222 + m123 * m232 + m124 * m242;
        this.rawData[2] = m121 * m213 + m122 * m223 + m123 * m233 + m124 * m243;
        this.rawData[3] = m121 * m214 + m122 * m224 + m123 * m234 + m124 * m244;

        this.rawData[0] = m131 * m211 + m132 * m221 + m133 * m231 + m134 * m241;
        this.rawData[1] = m131 * m212 + m132 * m222 + m133 * m232 + m134 * m242;
        this.rawData[2] = m131 * m213 + m132 * m223 + m133 * m233 + m134 * m243;
        this.rawData[3] = m131 * m214 + m132 * m224 + m133 * m234 + m134 * m244;

        this.rawData[0] = m141 * m211 + m142 * m221 + m143 * m231 + m144 * m241;
        this.rawData[1] = m141 * m212 + m142 * m222 + m143 * m232 + m144 * m242;
        this.rawData[2] = m141 * m213 + m142 * m223 + m143 * m233 + m144 * m243;
        this.rawData[3] = m141 * m214 + m142 * m224 + m143 * m234 + m144 * m244;
    }

    public float determinant {//方法:求矩阵的逆(未完成)
        get {
            return ((rawData[0] * rawData[5] - rawData[4] * rawData[1]) * (rawData[10] * rawData[15] - rawData[14] * rawData[11])
                  - (rawData[0] * rawData[9] - rawData[8] * rawData[1]) * (rawData[6] * rawData[15] - rawData[14] * rawData[7])
                  + (rawData[4] * rawData[9] - rawData[8] * rawData[5]) * (rawData[2] * rawData[15] - rawData[14] * rawData[3])
                  + (rawData[0] * rawData[13] - rawData[12] * rawData[1]) * (rawData[6] * rawData[11] - rawData[10] * rawData[7])
                  - (rawData[4] * rawData[13] - rawData[12] * rawData[5]) * (rawData[2] * rawData[11] - rawData[10] * rawData[3])
                  + (rawData[8] * rawData[13] - rawData[12] * rawData[9]) * (rawData[2] * rawData[7] - rawData[6] * rawData[3]));
            

              /*
                      0:();  1:();  2:();  3:();
                           X
                      4:();  5:();  6:();  7:();
                      8:();  9:();  10:(); 11:();
                                          X
                      12:(); 13:(); 14:(); 15:();
    */
        }
    }

    public void decompose(Vector3D vt,Vector3D vr,Vector3D vs) {//方法:获取矩阵的位移/旋转/缩放
        vt.x = rawData[12];
        vt.y = rawData[13];
        vt.z = rawData[14];

        Vector3D xaxis = new Vector3D(rawData[0], rawData[1], rawData[2]);
        float scaleX = xaxis.length;
        Vector3D yaxis = new Vector3D(rawData[4], rawData[5], rawData[6]);
        float scaleY = yaxis.length;
        Vector3D zaxis = new Vector3D(rawData[8], rawData[9], rawData[10]);
        float scaleZ = zaxis.length;

        if (this.determinant < 0) {
            scaleZ = -scaleZ;
        }
        vs.x = scaleX;
        vs.y = scaleY;
        vs.z = scaleZ;

        float rn = 0.0f;
        rn = 1.0f / scaleX;
        xaxis.scaleBy(rn);

        rn = 1.0f / scaleY;
        yaxis.scaleBy(rn);

        rn = 1.0f / scaleZ;
        zaxis.scaleBy(rn);

        vr.y = (float)System.Math.Asin(-xaxis.z);

        if (xaxis.z != 1 && xaxis.z != -1) {
            vr.x = (float)System.Math.Atan2(yaxis.z, zaxis.z);
            vr.z = (float)System.Math.Atan2(xaxis.y, xaxis.x);
        }
        else {
            vr.z = 0;
            vr.x = (float)System.Math.Atan2(yaxis.x, yaxis.y);
        }
    }

    public void invert() {//方法:实现对矩阵取逆
        float d = this.determinant;
        if (d < 0) {
            d = -d;
        }
        d = 1 / d;
        float m11 = rawData[0];
        float m21 = rawData[4];
        float m31 = rawData[8];
        float m41 = rawData[12];
        float m12 = rawData[1];
        float m22 = rawData[5];
        float m32 = rawData[9];
        float m42 = rawData[13];
        float m13 = rawData[2];
        float m23 = rawData[6];
        float m33 = rawData[10];
        float m43 = rawData[14];
        float m14 = rawData[3];
        float m24 = rawData[7];
        float m34 = rawData[11];
        float m44 = rawData[15];

        rawData[0] = d * (m22 * (m33 * m44 - m43 * m34) - m32 * (m23 * m44 - m43 * m24) + m42 * (m23 * m34 - m33 * m24));
        rawData[1] = -d * (m12 * (m33 * m44 - m43 * m34) - m32 * (m13 * m44 - m43 * m14) + m42 * (m13 * m34 - m33 * m14));
        rawData[2] = d * (m12 * (m23 * m44 - m43 * m24) - m22 * (m13 * m44 - m43 * m14) + m42 * (m13 * m24 - m23 * m14));
        rawData[3] = -d * (m12 * (m23 * m34 - m33 * m24) - m22 * (m13 * m34 - m33 * m14) + m32 * (m13 * m24 - m23 * m14));
        rawData[4] = -d * (m21 * (m33 * m44 - m43 * m34) - m31 * (m23 * m44 - m43 * m24) + m41 * (m23 * m34 - m33 * m24));
        rawData[5] = d * (m11 * (m33 * m44 - m43 * m34) - m31 * (m13 * m44 - m43 * m14) + m41 * (m13 * m34 - m33 * m14));
        rawData[6] = -d * (m11 * (m23 * m44 - m43 * m24) - m21 * (m13 * m44 - m43 * m14) + m41 * (m13 * m24 - m23 * m14));
        rawData[7] = d * (m11 * (m23 * m34 - m33 * m24) - m21 * (m13 * m34 - m33 * m14) + m31 * (m13 * m24 - m23 * m14));
        rawData[8] = d * (m21 * (m32 * m44 - m42 * m34) - m31 * (m22 * m44 - m42 * m24) + m41 * (m22 * m34 - m32 * m24));
        rawData[9] = -d * (m11 * (m32 * m44 - m42 * m34) - m31 * (m12 * m44 - m42 * m14) + m41 * (m12 * m34 - m32 * m14));
        rawData[10] = d * (m11 * (m22 * m44 - m42 * m14) - m21 * (m12 * m44 - m42 * m14) + m41 * (m12 * m24 - m22 * m14));
        rawData[11] = -d * (m11 * (m22 * m34 - m32 * m24) - m21 * (m12 * m34 - m32 * m14) + m31 * (m12 * m24 - m22 * m14));
        rawData[12] = -d * (m21 * (m32 * m43 - m42 * m33) - m31 * (m22 * m43 - m42 * m23) + m41 * (m22 * m33 - m32 * m23));
        rawData[13] = d * (m11 * (m32 * m43 - m42 * m33) - m31 * (m12 * m43 - m42 * m13) + m41 * (m12 * m33 - m32 * m13));
        rawData[14] = -d * (m11 * (m22 * m43 - m42 * m23) - m21 * (m12 * m43 - m42 * m13) + m41 * (m12 * m23 - m22 * m13));
        rawData[15] = d * (m11 * (m22 * m33 - m32 * m23) - m21 * (m12 * m33 - m32 * m13) + m31 * (m12 * m23 - m22 * m13));
    }

    public void getAxisRotation(float u, float v, float w, float a, float b, float c, float degress, Matrix3D m) {//通过给定的轴/角度得到一个矩阵
        float rad = degress / 180.0f * (float)System.Math.PI;//将角度转化为弧度

        float u2 = u * u;
        float v2 = v * v;
        float w2 = w * w;
        float l2 = u2 * v2 * w2;
        float l = (float)System.Math.Sqrt(l2);

        u /= l;
        v /= l;
        w /= l;

        float cos1 = (float)System.Math.Cos(rad);
        float sin1 = (float)System.Math.Sin(rad);

        m.rawData[0] = u2 + (v2 + w2) * cos1;
        m.rawData[1] = u * v * (1 - cos1) + w * sin1;
        m.rawData[2] = u * w * (1 - cos1) - v * sin1;
        m.rawData[3] = 0;

        m.rawData[4] = u * v * (1 - cos1) - w * sin1;
        m.rawData[5] = v2 + (u2 + w2) * cos1;
        m.rawData[6] = v * w * (1 - cos1) + u * sin1;
        m.rawData[7] = 0;

        m.rawData[8] = u * w * (1 - cos1) + v * sin1;
        m.rawData[9] = v * w * (1 - cos1) - u * sin1;
        m.rawData[10] = w2 + (u2 + w2) * cos1;
        m.rawData[11] = 0;

        m.rawData[12] = (a * (v2 + w2) - u * (b * v + c * w)) * (1 - cos1) + (b * w - c * v) * sin1;
        m.rawData[13] = (b * (u2 + w2) - u * (a * u + c * w)) * (1 - cos1) + (c * u - a * w) * sin1;
        m.rawData[14] = (c * (v2 + v2) - u * (a * u + b * v)) * (1 - cos1) + (a * v - b * u) * sin1;
        m.rawData[15] = 1;
    }

    private static Matrix3D _mt = new Matrix3D();

    public void appendRatation(float degress, Vector3D axis, Vector3D pivot = null) {
        if (pivot != null) {
            this.getAxisRotation(axis.x, axis.y, axis.z, pivot.x, pivot.y, pivot.z, degress, _mt);
        }
        else {
            this.getAxisRotation(axis.x, axis.y, axis.z, 0, 0, 0, degress, _mt);
        }
        this.append(_mt);
    }

    public void appendScale(float x,float y,float z) {
        float[] v = { x, 0, 0, 0, 0, y, 0, 0, 0, 0, z, 0, 0, 0, 0, 1 };
        _mt.copyForm(v);
        this.append(_mt);
    }

    public void setPosition(float x, float y, float z) {
        this.rawData[12] = x;
        this.rawData[13] = y;
        this.rawData[14] = z;
    }

    public void appendTranslation(float x,float y,float z) {
        this.rawData[12] += x;
        this.rawData[13] += y;
        this.rawData[14] += z;
    }

    public void recompose(Vector3D vt,Vector3D vr,Vector3D vs) {
        this.identity();
        this.appendScale(vs.x, vs.y, vs.z);

        float angle = 0.0f;
        angle = -vr.x;
        float[] v = { 1, 0, 0, 0, 0, (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0, 0, 0, 1 };
        _mt.copyForm(v);
        this.append(_mt);

        angle = -vr.z;
        float[] v0 = { (float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0, 0, 1, 0, 0, -(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0, 0, 0, 0, 1 };
        _mt.copyForm(v0);
        this.append(_mt);

        angle = -vr.z;
        float[] v1 = { (float)Math.Cos(angle), -(float)Math.Sin(angle), 0, 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        _mt.copyForm(v1);
        this.append(_mt);

        this.setPosition(vt.x, vt.y, vt.z);
        this.rawData[15] = 1.0f;
    }

    public void transpose() {
        float[] v = new float[16];
        for (int i = 0; i < 16; i++) {
            v[i] = rawData[i];
        }
        rawData[1] = v[4];
        rawData[2] = v[8];
        rawData[3] = v[12];
        rawData[4] = v[1];
        rawData[6] = v[9];
        rawData[7] = v[13];
        rawData[8] = v[2];
        rawData[9] = v[6];
        rawData[11] = v[14];
        rawData[12] = v[3];
        rawData[13] = v[7];
        rawData[14] = v[11];
    }

    public override string ToString() {
        return string.Format("" +
            rawData[0] + " " + rawData[1] + " " + rawData[2] + "  " + rawData[3] + "\n" +
            rawData[4] + " " + rawData[5] + " " + rawData[6] + "  " + rawData[7] + "\n" +
            rawData[8] + " " + rawData[9] + " " + rawData[10] + "  " + rawData[11] + "\n" +
            rawData[12] + " " + rawData[13] + " " + rawData[14] + "  " + rawData[15] + "\n" 
            );
    }

}
