using UnityEngine;

namespace NamesCode
{
    public static class TagExtensions
    {
        public static bool CompareTag(this GameObject go, TagName tagName)
        {
            return go.CompareTag(tagName.Name);
        }

        public static bool CompareTag(this Component c, TagName tagName)
        {
            return c.CompareTag(tagName.Name);
        }
    }
}