/****************************** Project Header ******************************\
Project:	      QuantLib Extension (QLExtension)
Author:			  Letian.zj@gmail.com
URL:			  https://github.com/letianzj/QLExtension
Copyright 2014 Letian_zj
This file is part of  QuantLib Extension (QLExtension) Project.
QuantLib Extension (QLExtension) is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
either version 3 of the License, or (at your option) any later version.
QuantTrading is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with QLExtension.
If not, see http://www.gnu.org/licenses/.
\***************************************************************************/

/*! \file svi.hpp
    \brief SVI functions
*/

#ifndef qlex_svi_hpp
#define qlex_svi_hpp

#include <ql/types.hpp>
using namespace QuantLib;

namespace QLExtension {

    Real unsafeSviVolatility(Rate strike,
                              Rate forward,
                              Time expiryTime,
							  Real a,
							  Real b,
							  Real rho,
							  Real m,
							  Real sigma);

    Real sviVolatility(Rate strike,
                        Rate forward,
                        Time expiryTime,
						Real a,
						Real b,
						Real rho,
						Real m,
						Real sigma);

    void validateSviParameters(Real a,
                                Real b,
								Real rho,
								Real m,
								Real sigma);

}

#endif
