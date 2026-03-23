using System;
using UnityEngine;

// IProgress 는 
// 자료형을 자유롭게 쓸 수 있는것 => 제네릭 메소드
// 제레릭 클래스
// 메소는 클래스 안에

// IProgress => I는 인터페이스라는 표시 Progress 는 진행도라는 뜻 // 진행상황을 표현하기 위한 규칙
public interface IProgress<T> // <T> => 클레스를 만들때 원해는 타입으로 만들 수 있다
{
    public T Current { get; }
    public T Max { get; }

    public float Progress { get; }

    public T Set(T newCurrent);
    public T Set(T newCurrent, T newMax);

    public T AddCurrent(T value);
    public T AddMax(T value); 
}