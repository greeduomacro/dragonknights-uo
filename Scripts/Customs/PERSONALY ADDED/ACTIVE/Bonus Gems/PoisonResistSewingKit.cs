using Server.Targeting;

namespace Server.Items
{
	public class PoisonResistSewingKit : Item, IUsesRemaining
	{
        private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

        [Constructable]
        public PoisonResistSewingKit() : this(1)
		{
		}

        [Constructable]
		public PoisonResistSewingKit( int uses ) : base( 0xF9D )
		{
			m_UsesRemaining = uses;
            Name = "Poison resist sewing kit";
            Hue = 1155;
		}

        public bool ShowUsesRemaining { get { return true; } set { } }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, m_UsesRemaining.ToString()); // uses remaining: ~1_val~
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.Target = new SewingTarget(this);
                from.SendMessage("Target a piece of clothes (including footwears) that you wish to add poison resist to.");
            }

            else
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        public PoisonResistSewingKit(Serial serial)
            : base(serial)
		{
		}

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		    m_UsesRemaining = reader.ReadInt();
		}

        private class SewingTarget : Target
        {
            private PoisonResistSewingKit m_Kit;

            public SewingTarget(PoisonResistSewingKit kit)
                : base(30, false, TargetFlags.None)
            {
                m_Kit = kit;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                BaseClothing Clothtarg = target as BaseClothing;
                if (Clothtarg == null)
                {
                    from.SendMessage("That is not a piece of clothes.");
                    return;
                }

                if (!Clothtarg.IsChildOf(from.Backpack))
                {
                    from.SendLocalizedMessage(1061005); // The item must be in your backpack to enhance it.
                    return;
                }

                if (Clothtarg.PoisonResistance > 1)
                {
                    from.SendMessage("You can not enhance that item any further in poison resists.");
                    return;
                }

                if (Clothtarg.PoisonResistance > 0 && from.Skills[SkillName.Tailoring].Value < 90)
                {
                    from.SendMessage("You need 90 in tailoring to further enhance that item in poison resists.");
                    return;
                }

                if (m_Kit != null && !m_Kit.Deleted && m_Kit.IsChildOf(from))
                {
                    Clothtarg.Resistances.Poison += 1;
                    from.SendMessage("You have enhanced the item in poison resists.");
                    m_Kit.UsesRemaining--;
                    Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x376A, 1, 29, 0x47D, 2, 9962, 0);
                    Effects.SendLocationParticles(EffectItem.Create(new Point3D(from.X, from.Y, from.Z - 7), from.Map, EffectItem.DefaultDuration), 0x37C4, 1, 29, 0x47D, 2, 9502, 0);
                    from.PlaySound(0x212);
                    from.PlaySound(0x206);
                }

                if (m_Kit.UsesRemaining < 1)
                {
                    m_Kit.Delete();
                    from.SendMessage("The poison resist sewing kit is used up.");
                }
            }
        }
    }
}
