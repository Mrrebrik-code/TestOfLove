using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Category", menuName = "Custom/Category")]
public class Category : ScriptableObject
{
	[SerializeField] private Categorys _categorys;
	[SerializeField] private List<Question> _questions;
	[SerializeField] private List<Question> _additionalQuestions;
	public Categorys Categorys { get { return _categorys; } }
	public List<Question> Questions { get { return _questions; } }
	public List<Question> AdditionalQuestions { get { return _additionalQuestions; } }
}
