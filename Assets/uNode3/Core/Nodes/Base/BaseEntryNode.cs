﻿using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace MaxyGames.UNode {
	/// <summary>
	/// Base class for all entry node.
	/// </summary>
	public abstract class BaseEntryNode : Node {
		public override string GetTitle() {
			return "Entry";
		}

		public override Type GetNodeIcon() {
			return typeof(TypeIcons.FlowIcon);
		}
	}
}