using System;
using System.Collections.Generic;
using System.Linq;

namespace AndiSoft.Utilities.Extensions
{
    public static class ListExtension
    {
        ///<summary>
        ///Checks whether two lists have at least one element in common.
        ///<para/>
        ///The method Object.Equals is used to compare both elements.
        ///</summary>
        public static bool AnyEqualElement<T>(this List<T> thisList, List<T> listToCompare)
        {
            foreach (var elementA in thisList)
            {
                foreach (var elementB in listToCompare)
                {
                    if (elementA.Equals(elementB)) return true;
                }
            }
            return false;
        }

        ///<summary>
        ///Checks whether two lists have at least one element in common.
        ///<para/>
        ///The <code>areEqualFunc</code> parameter is a function used to compare both elements.
        ///</summary>
        public static bool AnyEqualElement<T>(this List<T> thisList, List<T> listToCompare, Func<T,T, bool> areEqualFunc)
        {
            return thisList.Any(elementA => listToCompare.Any(elementB => areEqualFunc(elementA, elementB)));
        }
    }
}