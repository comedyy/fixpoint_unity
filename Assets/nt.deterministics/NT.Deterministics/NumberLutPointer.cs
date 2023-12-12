
using System;
using System.Runtime.CompilerServices;
using System.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#if Debug || DEBUG
using System.Diagnostics;
#endif

namespace Nt.Deterministics
{
#if Debug || DEBUG
    [DebuggerDisplay("Length = {m_Length}")]
    [DebuggerTypeProxy(typeof(LutPointerDebugView))]
#endif
    public struct LutPointer : IDisposable, IEnumerable, IEquatable<LutPointer>
    {
        [NativeDisableUnsafePtrRestriction]
        internal unsafe long* lut;
        private int m_Length;
        private Allocator m_AllocatorLabel;

        public unsafe long this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => lut[index];
            [WriteAccessRequired]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (index >= 0 && index < m_Length)
                    lut[index] = value;
            }
        }
        public int length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.m_Length;
        }
        public unsafe bool isCreated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.lut != null;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Allocate(int length)
        {
            long totalSize = (long)UnsafeUtility.SizeOf<long>() * (long)length;
            this.m_AllocatorLabel = Allocator.Persistent;
            this.m_Length = length;
            this.lut = (long*)UnsafeUtility.Malloc(totalSize, UnsafeUtility.AlignOf<long>(), m_AllocatorLabel);
            UnsafeUtility.MemClear(this.lut, totalSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Allocate(long[] data)
        {
            if (null == data) throw new Exception("LutPointer::Allocate - data is null");
            long totalSize = (long)UnsafeUtility.SizeOf<long>() * (long)data.Length;
            this.m_AllocatorLabel = Allocator.Persistent;
            this.m_Length = data.Length;
            this.lut = (long*)UnsafeUtility.Malloc(totalSize, UnsafeUtility.AlignOf<long>(), m_AllocatorLabel);
            fixed (long* p = &(data[0]))
                UnsafeUtility.MemCpy(this.lut, p, totalSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Allocate(byte[] data)
        {
            if (null == data) throw new Exception("LutPointer::Allocate - data is null");
            long totalSize = (long)data.Length;
            this.m_AllocatorLabel = Allocator.Persistent;
            this.m_Length = data.Length / UnsafeUtility.SizeOf<long>();
            this.lut = (long*)UnsafeUtility.Malloc(totalSize, UnsafeUtility.AlignOf<long>(), m_AllocatorLabel);
            fixed (byte* p = &(data[0]))
                UnsafeUtility.MemCpy(this.lut, p, totalSize);
        }

        [WriteAccessRequired]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Dispose()
        {
            if (this.lut == null) throw new ObjectDisposedException("The LutPointer is already disposed.");
            if (this.m_AllocatorLabel == Allocator.Invalid)
                throw new InvalidOperationException("The LutPointer can not be Disposed because it was not allocated with a valid allocator.");
            if (this.m_AllocatorLabel > Allocator.None)
            {
                UnsafeUtility.Free(this.lut, this.m_AllocatorLabel);
                this.m_AllocatorLabel = Allocator.Invalid;
            }
            this.lut = null;
            this.m_Length = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe bool Equals(LutPointer other) => this.lut == other.lut && this.m_Length == other.m_Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator GetEnumerator() => new LutPointerEnumerator(ref this);

        public struct LutPointerEnumerator : IEnumerator, IDisposable
        {
            public unsafe LutPointerEnumerator(ref LutPointer pointer)
            {
                this.m_Pointer = pointer;
                this.m_Index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public unsafe void Dispose()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                this.m_Index++;
                return this.m_Index < this.m_Pointer.m_Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Reset() => this.m_Index = -1;

            public unsafe long Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => this.m_Pointer[this.m_Index];
            }

            object IEnumerator.Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => this.Current;
            }

            private LutPointer m_Pointer;
            private int m_Index;
        }
        public sealed class LutPointerDebugView
        {
            LutPointer m_Pointer;
            public LutPointerDebugView(LutPointer pointer)
            {
                m_Pointer = pointer;
            }

            public unsafe long[] Items
            {
                get
                {
                    var array = new long[m_Pointer.length];
                    if (m_Pointer.length > 0)
                    {
                        fixed (long* p = &(array[0]))
                        {
                            UnsafeUtility.MemCpy(p, m_Pointer.lut, (long)UnsafeUtility.SizeOf<long>() * m_Pointer.length);
                        }
                    }
                    return array;
                }
            }

            public Allocator allocator => m_Pointer.m_AllocatorLabel;
            public int length => m_Pointer.m_Length;
        }
    }
}
