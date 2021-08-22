using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Nanory.Unity.Entities.Unsafe
{
    public static unsafe class InterpretUnsafeUtility
    {
        public static ref TRetrieve Retrieve<TInput, TRetrieve>(ref TInput inst) where TInput : struct where TRetrieve : struct
        {
#if ENTITIES_011_2
            return ref UnsafeUtilityEx.As<TInput, TRetrieve>(ref inst);
#else
            return ref UnsafeUtility.As<TInput, TRetrieve>(ref inst);
#endif
        }

        public static ref TRetrieve Retrieve<TInput, TRetrieve>(ref TInput inst, int offset) where TInput : struct where TRetrieve : struct
        {
            var pt = UnsafeUtility.AddressOf(ref inst);
#if ENTITIES_011_2
            return ref UnsafeUtilityEx.AsRef<TRetrieve>(IntPtr.Add((IntPtr) pt, offset).ToPointer());
#else
            return ref UnsafeUtility.AsRef<TRetrieve>(IntPtr.Add((IntPtr) pt, offset).ToPointer());
#endif
        }
    }
}

