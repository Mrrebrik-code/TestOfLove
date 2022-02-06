using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrollObject", menuName = "Custom/ScrollObejct")]
public partial class ScrollObejct : ScriptableObject
{
	[SerializeField] private int _id;
	[SerializeField] private TypeMode _typeMode;
	[SerializeField] private string _nameMode;
	[SerializeField] private Sprite _icon;
	[SerializeField] private Categorys _category;
	[SerializeField] private ModeHolder _modePrefab;
	[SerializeField] private StatusMode.Style.Type _type;

	public int Id { get { return _id; } }
	public string NameMode { get { return _nameMode; } }
	public Sprite Icon { get { return _icon;} }
	public Categorys Category { get { return _category; } }
	public ModeHolder ModePrefab { get { return _modePrefab;} }
	public StatusMode.Style.Type Type { get { return _type; } }
	public TypeMode TypeMode { get { return _typeMode;} }
}
