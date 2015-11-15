﻿/*
    Copyright 2014 Björn Fyrvall

    This file is part of BonaJson

    BonaJson is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    BonaJson is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with BonaJson.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonaJson
{
    public class JValueObject : Internal.JObjectValue
    {
        public JValueObject(float value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            float value = (float)m_value;
            return m_value.ToString();
        }

        public override T Value<T>()
        {
            return (T)m_value;
        }

        public override float GetAsFloat()
        {
            return (float)m_value;
        }
    }
}