using System.Collections;
using System.Threading.Tasks;
using System.Xml.Schema;
using Unity.Mathematics.Geometry;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
// 게임으로 따지면 확장팩(DLC)
// => 추가 컨텐츠
// 원본에 없는것을 추가해주는 개념
// 원하는 클래스에 원하는 기능 추가

public static class Extensions
{
    // Nomalize  => 정규화 => 방향을 유지한 상태로 크기를 1로 만드는것
    // Nomalized => 크기가 1인 값을 돌려주는 것
    // + 정규화 : +@ =  1
    // - 정규화 : -@ = -1
    // 0 정규화 :  0 =  0
    public static float nomalized(this float target) // this => 내가 함수를 넣고싶은 대상
    {

        if      (target > 0) { return  1; }
        else if (target < 0) { return -1; }
        else                 { return  0; }
    }

    // Try Add Component => 추가를 시도 => 있는지 확인 => 없으면 추가
    public static T TryAddComponent<T>(this GameObject target) where T : Component
    {
        T result = null;
        
        if (target == null) return result;

        result = target.GetComponent<T>() ?? target.AddComponent<T>(); // [Get] 이 [??] 없다면 [Add] 를 써라
        
        // if (result is null) { result = target.AddComponent<T>(); }

        // 2번과 3번을 합치면(한줄로 줄인다면) 1번이 됨
        // 1. result = target.GetComponent<T>() ?? target.AddComponent<T>();
        // 2. result ??= target.AddComponent<T>(); // [??]없다면(null 이라면) [=]넣어라
        // 3. if (result is null) { result = target.AddComponent<T>(); }

        // return target.GetComponent<T>();

        return result;
    }

    public static T TryAddComponent<T>(this Transform target) where T : Component // NRVO => 이름이 있는 반환값 최적화 => 컴파일러가 최적화해서 반환값을 바로 반환해주는 것
    {
        if (target == null) return null;
        else                return target.TryAddComponent<T>(); // NRVO
    }

    public static IEnumerator WiatForTask(this Task targetTask) // Task => 비동기 작업 => 끝날때 까지 기다리는 함수
    {
        yield return new WaitUntil(() => targetTask.IsCompleted); // Task가 끝날때 까지 기다려라
        targetTask.Dispose(); // Task는 끝났으면 닫아줘야함
    }

    public static T TryAddComponent<T>(this Component target) where T : Component
    {
        if (target == null) return null;
        else return target.gameObject.TryAddComponent<T>(); // NRVO
    }



    public static float GetPenetratedDostance(float aHalf, float bHalf, float aPos, float bPos)
    {
        float absAHalf = Mathf.Abs(aHalf);
        float absBHalf = Mathf.Abs(bHalf);

        float minSpace = absAHalf + absBHalf;
        // 둘 사이의 거리가 얼마나 가까운지
        float distance = aPos - bPos;
        // x최소 거리와 둘사이의 거리 차이
        float penetration = minSpace - Mathf.Abs(distance);

        penetration *= Mathf.Sign(penetration);
        return penetration;
    }

    public static Vector2 AABB(this Rect A, Rect B)
    {
        Vector2 result = Vector2.zero;
        Vector2 aMin = A.min;
        Vector2 aMax = A.max;
        Vector2 aHalf = A.size * .5f;
        Vector2 bMin = B.min;
        Vector2 bMax = B.max;
        Vector2 bHalf = B.size * .5f;

        if (aMax.x > bMin.x && bMax.x > aMin.x) result.x = GetPenetratedDostance(aHalf.x, bHalf.x, A.position.x, B.position.x);
        if (aMax.y > bMin.y && bMax.y > aMin.y) result.y = GetPenetratedDostance(aHalf.y, bHalf.y, A.position.y, B.position.y);

        return result;
    }

    public static float GetOutboundDistance(float inMin, float outMin, float inMax, float outMax)
    {
        float result = 0.0f;

        bool leftOut = inMin < outMin;
        bool rightOut = inMax > outMax;


        if (leftOut ^ rightOut)
        {
            if (leftOut) result = outMin - inMin;
            if (rightOut) result = outMax - inMax;
        }

        return result;
    }

    public static Vector2 InversedAABB(this Rect target, Rect bound)
    {
        Vector2 result;
        result.x = GetOutboundDistance(target.xMin, bound.xMin, target.xMax, bound.xMax);
        result.y = GetOutboundDistance(target.yMin, bound.yMin, target.yMax, bound.yMax);
        return result;
    }

}

