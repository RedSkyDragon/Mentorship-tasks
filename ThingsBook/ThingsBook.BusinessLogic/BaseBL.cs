﻿using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Base class for business logic implementation.
    /// </summary>
    public class BaseBL
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        protected Storage Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBL"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public BaseBL(Storage data)
        {
            Data = data;
        }
    }
}
