﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Difficulty
{
    /// <summary>
    /// Describes the difficulty of a beatmap, as output by a <see cref="DifficultyCalculator"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DifficultyAttributes
    {
        protected const int ATTRIB_ID_AIM = 1;
        protected const int ATTRIB_ID_SPEED = 3;
        protected const int ATTRIB_ID_OVERALL_DIFFICULTY = 5;
        protected const int ATTRIB_ID_APPROACH_RATE = 7;
        protected const int ATTRIB_ID_MAX_COMBO = 9;
        protected const int ATTRIB_ID_DIFFICULTY = 11;
        protected const int ATTRIB_ID_GREAT_HIT_WINDOW = 13;
        protected const int ATTRIB_ID_SCORE_MULTIPLIER = 15;
        protected const int ATTRIB_ID_FLASHLIGHT = 17;
        protected const int ATTRIB_ID_SLIDER_FACTOR = 19;
        protected const int ATTRIB_ID_SPEED_NOTE_COUNT = 21;
        protected const int ATTRIB_ID_LEGACY_TOTAL_SCORE = 23;
        protected const int ATTRIB_ID_LEGACY_COMBO_SCORE = 25;
        protected const int ATTRIB_ID_LEGACY_BONUS_SCORE = 27;

        /// <summary>
        /// The mods which were applied to the beatmap.
        /// </summary>
        public Mod[] Mods { get; set; } = Array.Empty<Mod>();

        /// <summary>
        /// The combined star rating of all skills.
        /// </summary>
        [JsonProperty("star_rating", Order = -7)]
        public double StarRating { get; set; }

        /// <summary>
        /// The maximum achievable combo.
        /// </summary>
        [JsonProperty("max_combo", Order = -6)]
        public int MaxCombo { get; set; }

        /// <summary>
        /// The maximum achievable legacy total score.
        /// </summary>
        [JsonProperty("legacy_total_score", Order = -5)]
        public int LegacyTotalScore { get; set; }

        /// <summary>
        /// The combo-multiplied portion of <see cref="LegacyTotalScore"/>.
        /// </summary>
        [JsonProperty("legacy_combo_score", Order = -4)]
        public int LegacyComboScore { get; set; }

        /// <summary>
        /// The "bonus" portion of <see cref="LegacyTotalScore"/> consisting of all judgements that would be <see cref="HitResult.SmallBonus"/> or <see cref="HitResult.LargeBonus"/>.
        /// </summary>
        [JsonProperty("legacy_bonus_score", Order = -3)]
        public int LegacyBonusScore { get; set; }

        /// <summary>
        /// Creates new <see cref="DifficultyAttributes"/>.
        /// </summary>
        public DifficultyAttributes()
        {
        }

        /// <summary>
        /// Creates new <see cref="DifficultyAttributes"/>.
        /// </summary>
        /// <param name="mods">The mods which were applied to the beatmap.</param>
        /// <param name="starRating">The combined star rating of all skills.</param>
        public DifficultyAttributes(Mod[] mods, double starRating)
        {
            Mods = mods;
            StarRating = starRating;
        }

        /// <summary>
        /// Converts this <see cref="DifficultyAttributes"/> to osu-web compatible database attribute mappings.
        /// </summary>
        /// <remarks>
        /// See: osu_difficulty_attribs table.
        /// </remarks>
        public virtual IEnumerable<(int attributeId, object value)> ToDatabaseAttributes()
        {
            yield return (ATTRIB_ID_MAX_COMBO, MaxCombo);
            yield return (ATTRIB_ID_LEGACY_TOTAL_SCORE, LegacyTotalScore);
            yield return (ATTRIB_ID_LEGACY_COMBO_SCORE, LegacyComboScore);
            yield return (ATTRIB_ID_LEGACY_BONUS_SCORE, LegacyBonusScore);
        }

        /// <summary>
        /// Reads osu-web database attribute mappings into this <see cref="DifficultyAttributes"/> object.
        /// </summary>
        /// <param name="values">The attribute mappings.</param>
        /// <param name="onlineInfo">The <see cref="IBeatmapOnlineInfo"/> where more information about the beatmap may be extracted from (such as AR/CS/OD/etc).</param>
        public virtual void FromDatabaseAttributes(IReadOnlyDictionary<int, double> values, IBeatmapOnlineInfo onlineInfo)
        {
            MaxCombo = (int)values[ATTRIB_ID_MAX_COMBO];
            LegacyTotalScore = (int)values[ATTRIB_ID_LEGACY_TOTAL_SCORE];
            LegacyComboScore = (int)values[ATTRIB_ID_LEGACY_COMBO_SCORE];
            LegacyBonusScore = (int)values[ATTRIB_ID_LEGACY_BONUS_SCORE];
        }
    }
}
