using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Nanory.Unity.Entities.Unsafe
{
    public static unsafe class InterpretUnsafeUtility
    {
        public static ref TRetrieve Retrieve<TInput, TRetrieve>(ref TInput inst) where TInput : struct where TRetrieve : struct
        {
            return ref UnsafeUtilityEx.As<TInput, TRetrieve>(ref inst);
        }

        public static ref TRetrieve Retrieve<TInput, TRetrieve>(ref TInput inst, int offset) where TInput : struct where TRetrieve : struct
        {
            var pt = UnsafeUtility.AddressOf(ref inst);
            return ref UnsafeUtilityEx.AsRef<TRetrieve>(IntPtr.Add((IntPtr) pt, offset).ToPointer());
        }
    }
}

