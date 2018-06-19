using System.Collections;
using System.Collections.Generic;
using System;

public class Matrix3DUtil {

    private static Vector3D _vector = new Vector3D();
    private static Vector3D _right  = new Vector3D();
    private static Vector3D _up     = new Vector3D();
    private static Vector3D _dir = new Vector3D();
    private static Vector3D _scale = new Vector3D();
    private static Vector3D _pos = new Vector3D();

    private static float _toRad = 0.0174532925199433f;
    private static float _toAng = 57.2957795130823f;

    private static float[] _raw = new float[16];

    public static void getRight(Matrix3D m, Vector3D v) {
        m.copyColumnTo(0, v);
    }
    public static void getUp(Matrix3D m, Vector3D v) {
        m.copyColumnTo(1, v);
    }
    public static void getDir(Matrix3D m, Vector3D v) {
        m.copyColumnTo(2, v);
    }
    public static void getLeft(Matrix3D m, Vector3D v) {
        m.copyColumnTo(0, v);
        v.negate();
    }
    public static void getBackward(Matrix3D m, Vector3D v) {
        m.copyColumnTo(2, v);
        v.negate();
    }
    public static void getDown(Matrix3D m, Vector3D v) {
        m.copyColumnTo(2, v);
        v.negate();
    }

    public static void getPosition(Matrix3D m, Vector3D v) {
        m.copyColumnTo(3, v);
    }

    public static void getScale(Matrix3D m, Vector3D v) {
        m.copyColumnTo(0, _right);
        m.copyColumnTo(1, _up);
        m.copyColumnTo(2, _dir);
        v.x = _right.length;
        v.y = _up.length;
        v.z = _dir.length;
    }

    public static void  setPosition(Matrix3D m, float x, float y, float z) {
        _vector.setTo(x, y, z);
        _vector.w = 1.0f;
        m.copyColumnForm(3, _vector);
    }

    public static void setVectors(Matrix3D m, Vector3D right, Vector3D up, Vector3D dir) {
        m.copyColumnForm(0, right);
        m.copyColumnForm(1, up);
        m.copyColumnForm(2, dir);
    }

    public static void setOrientation(Matrix3D m, Vector3D dir, Vector3D up) {
        getScale(m, _scale);
        Vector3D upp = new Vector3D();
        Vector3D dirr = new Vector3D();
        upp.setTo(up);
        dirr.setTo(dir);

        dirr.normalize();
        Vector3D rVec = upp.crossProduct(dirr);
        rVec.normalize();
        Vector3D uVec = dirr.crossProduct(rVec);
        rVec.scaleBy(_scale.x);
        uVec.scaleBy(_scale.y);
        dirr.scaleBy(_scale.z);

        rVec.w = 0.0f;
        uVec.w = 0.0f;
        dirr.w = 0.0f;
        setVectors(m, rVec, uVec, dirr);
    }

    public static void lookAt(Matrix3D m, float x, float y, float z, Vector3D up) {
        m.copyColumnTo(3, _pos);
        _vector.x = x - _pos.x;
        _vector.y = y - _pos.y;
        _vector.z = z - _pos.z;
        setOrientation(m, _vector, up);
    }

    public static void translateX(Matrix3D m, float distance) {
        m.copyColumnTo(3, _pos);
        m.copyColumnTo(0, _right);
        _pos.x += distance * _right.x;
        _pos.y += distance * _right.y;
        _pos.z += distance * _right.z;
        m.copyColumnForm(3, _pos);
    }
    public static void translateY(Matrix3D m, float distance) {
        m.copyColumnTo(3, _pos);
        m.copyColumnTo(1, _up);
        _pos.x += distance * _up.x;
        _pos.y += distance * _up.y;
        _pos.z += distance * _up.z;
        m.copyColumnForm(3, _pos);
    }
    public static void translateZ(Matrix3D m, float distance) {
        m.copyColumnTo(3, _pos);
        m.copyColumnTo(2, _dir);
        _pos.x += distance * _dir.x;
        _pos.y += distance * _dir.y;
        _pos.z += distance * _dir.z;
        m.copyColumnForm(3, _pos);
    }
    public static void translateAxis(Matrix3D m, Vector3D axis, float distance) {
        m.copyColumnTo(3, _pos);
        _pos.x += distance * axis.x;
        _pos.y += distance * axis.y;
        _pos.z += distance * axis.z;
        m.copyColumnForm(3, _pos);
    }

    public static void setScale(Matrix3D m, float x, float y, float z) {
        getScale(m, _scale);
        float _x = _scale.x;
        float _y = _scale.y;
        float _z = _scale.z;
        _scale.x += (x - _scale.x);
        _scale.y += (y - _scale.y);
        _scale.z += (z - _scale.z);
        _right.scaleBy(_scale.x / _x);
        _up.scaleBy(_scale.y / _y);
        _dir.scaleBy(_scale.z / _z);
        setVectors(m, _right, _up, _dir);
    }

    public static void scaleX(Matrix3D m, float scale) {
        m.copyColumnTo(0, _right);
        _right.normalize();
        _right.scaleBy(scale);
        m.copyColumnForm(0, _right);
    }

    public static void scaleY(Matrix3D m, float scale) {
        m.copyColumnTo(1, _up);
        _up.normalize();
        _up.scaleBy(scale);
        m.copyColumnForm(1, _up);
    }

    public static void scaleZ(Matrix3D m, float scale) {
        m.copyColumnTo(2, _dir);
        _dir.normalize();
        _dir.scaleBy(scale);
        m.copyColumnForm(2, _dir);
    }

    public static void getRotation(Matrix3D m, Vector3D vec) {//获取角度
        m.decompose(_right, _vector, _dir);
        vec.x = _vector.x * _toAng;
        vec.y = _vector.y * _toAng;
        vec.z = _vector.z * _toAng;
        m.recompose(_right, _vector, _dir);
    }


    public static void setRotation(Matrix3D m, float x, float y, float z) {//设置角度
        m.decompose(_right, _vector, _dir);
        _vector.x = x * _toRad;
        _vector.y = y * _toRad;
        _vector.z = z * _toRad;
        m.recompose(_right, _vector, _dir);
    }

    public static void rotateAxis(Matrix3D m, float angle, Vector3D axis, Vector3D pivot = null) {
        _vector.x = axis.x;
        _vector.y = axis.y;
        _vector.z = axis.z;
        _vector.normalize();
        m.copyColumnTo(3, _pos);
        if (pivot != null) {
            m.appendRatation(angle, _vector, pivot);
        }
        else {
            m.appendRatation(angle, _vector, _pos);
        }
    }

    public static void rotateX(Matrix3D m, float angle, bool local, Vector3D pivot) {
        if (local) {
            getRight(m, _vector);
            rotateAxis(m, angle, _vector, pivot);
        }
        else {
            rotateAxis(m, angle, Vector3D.x_axis, pivot);
        }
    }

    public static void rotateY(Matrix3D m, float angle, bool local, Vector3D pivot) {
        if (local) {
            getRight(m, _vector);
            rotateAxis(m, angle, _vector, pivot);
        }
        else {
            rotateAxis(m, angle, Vector3D.x_axis, pivot);
        }
    }

    public static void rotateZ(Matrix3D m, float angle, bool local, Vector3D pivot) {
        if (local) {
            getRight(m, _vector);
            rotateAxis(m, angle, _vector, pivot);
        }
        else {
            rotateAxis(m, angle, Vector3D.x_axis, pivot);
        }
    }

    public static void transfromVector(Matrix3D m, Vector3D source, Vector3D dest) {
        _vector.setTo(source);
        m.copyRowTo(0, _right);
        m.copyRowTo(1, _up);
        m.copyRowTo(2, _dir);
        m.copyColumnTo(3, dest);
        dest.x += _vector.x * _right.x + _vector.y * _right.y + _vector.z * _right.z;
        dest.y += _vector.x * _up.x + _vector.y * _up.y + _vector.z * _up.z;
        dest.z += _vector.x * _dir.x + _vector.y * _dir.y + _vector.z * _dir.z;
    }

    public static void deltaTransformVector(Matrix3D m, Vector3D source, Vector3D dest) {
        _vector.setTo(source);
        m.copyRowTo(0, _right);
        m.copyRowTo(1, _up);
        m.copyRowTo(2, _dir);
        m.copyColumnTo(3, dest);
        dest.x = _vector.x * _right.x + _vector.y * _right.y + _vector.z * _right.z;
        dest.y = _vector.x * _up.x + _vector.y * _up.y + _vector.z * _up.z;
        dest.z = _vector.x * _dir.x + _vector.y * _dir.y + _vector.z * _dir.z;
    }

}
