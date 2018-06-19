using System.Collections;
using System.Collections.Generic;

public class Vector3D {
    public static readonly Vector3D x_axis = new Vector3D(1, 0, 0);//设定空间轴
    public static readonly Vector3D y_axis = new Vector3D(0, 1, 0);
    public static readonly Vector3D z_axis = new Vector3D(0, 0, 1);

    public float x;//1.设定xyzw作为向量
    public float y;
    public float z;
    public float w;

    public Vector3D(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 1.0f) {//实现快速设置的函数后,用快速设置来赋值
        this.setTo(x, y, z, w);
    }//实现构造函数(可以接受四个参数),分别设置向量四个维度(默认向量为一个点)

    public Vector3D(Vector3D vec) {//再次编写构造函数,使其可以接受Vector3D的值并赋值四个维度
        this.setTo(vec);
    }

    public void setTo(Vector3D vec) {//对setTo再次实现,使其可以接受vector3D的数据
        this.x = vec.x;
        this.y = vec.y;
        this.z = vec.z;
        this.w = vec.w;
    }
    public void setTo(float x, float y, float z, float w = 1.0f) {//实现快速设置的函数
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public bool equal(Vector3D vec) {//编写函数:判断两个Vector3D是否相等,返回bool值
        if (this.x == vec.x && this.y == vec.y && this.z == vec.z && this.w == vec.w) {
            return true;
        }
        return false;
    }

    public float lengthSquared {//编写函数:求向量各个坐标平方和
        get {
            return x * x + y * y + z * z;
        }
    }

    public float length {//编写函数:求向量长度(即将平方和开方)
        get {
            return (float)System.Math.Sqrt(this.lengthSquared);
        }
    }

    public float dot(Vector3D v) {//编写方法:实现向量的点积
        return x * v.x + y * v.y + z * v.z;
    }

    public Vector3D crossProduct(Vector3D vec) {//编写方法:实现向量的叉积
        Vector3D ret = new Vector3D();
        ret.x = y * vec.z - z * vec.y;
        ret.y = z * vec.x - x * vec.z;
        ret.z = x * vec.y - y * vec.x;
        return ret;
    }

    public Vector3D add(Vector3D vec) {//编写方法:实现向量的加减法
        Vector3D ret = new Vector3D();
        ret.x += vec.x;
        ret.y += vec.y;
        ret.z += vec.z;
        return ret;
    }

    public Vector3D sub(Vector3D vec) {
        Vector3D ret = new Vector3D();
        ret.x -= vec.x;
        ret.y -= vec.y;
        ret.z -= vec.z;
        return ret;
    }

    public void negate() {//编写方法:实现向量的取反
        this.x = -this.x;
        this.y = -this.y;
        this.z = -this.z;
    }

    public void scaleBy(float f) {//编写方法:实现向量的缩放
        this.x *= f;
        this.y *= f;
        this.z *= f;
    }

    public void normalize() {//编写方法:实现向量的规范化(单位化)
        float invlength = 0.0f;
        if (this.length==0) {
            invlength = 0.0f;
        }
        else {
            invlength = 1.0f / length;
        }
        this.x *= invlength;
        this.y *= invlength;
        this.z *= invlength;
    }

    public override string ToString() {
        return string.Format("(" + x + "," + y + "," + z + ")");
    }
}
