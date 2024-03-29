﻿//
// System.Runtime.Serialization.SerializationInfoEnumerator.cs
//
// Author: Duncan Mak (duncan@ximian.com)
//
// (C) Ximian, Inc.
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Runtime.Serialization
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public sealed class SerializationInfoEnumerator : IEnumerator
    {
        IEnumerator enumerator;

        // Constructor
        internal SerializationInfoEnumerator(IEnumerable<SerializationEntry> list)
        {
            this.enumerator = list.GetEnumerator();
        }

        // Properties
        public SerializationEntry Current
        {
            get { return (SerializationEntry)enumerator.Current; }
        }

        object IEnumerator.Current
        {
            get { return enumerator.Current; }
        }

        public string Name
        {
            get { return this.Current.Name; }
        }

        public Type ObjectType
        {
            get { return this.Current.ObjectType; }
        }

        public object Value
        {
            get { return this.Current.Value; }
        }

        // Methods
        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public void Reset()
        {
            enumerator.Reset();
        }
    }
}
