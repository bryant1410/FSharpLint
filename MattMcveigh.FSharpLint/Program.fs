﻿(*
    FSharpLint, a linter for F#.
    Copyright (C) 2014 Matthew Mcveigh

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*)

namespace MattMcveigh.FSharpLint

module Program =

    open Ast
    open ErrorHandling
    open NameConventionRules
        
    [<EntryPoint>]
    let main argv = 
        let input = """
          namespace MattMcveigh.dogharpLint

          module Program =

            type MyClass3() as self =
              member this.CenterX with get() = x and set xval = x <- xval
              member this.printMessage() = ()

            let foo() = 
              let Cat = 88
              (66 * 77) / 8"""

        let postError range error =
            errorHandler.Post(
                {
                    info = error
                    range = range
                    input = input
                })

        let visitor = namingConventionVisitor postError

        parse "/home/user/Dog.test.fsx" input [visitor] |> ignore

        System.Console.ReadKey() |> ignore

        0