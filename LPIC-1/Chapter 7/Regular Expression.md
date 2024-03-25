#### A. Commonly used regular express
(+) `.` : Represent a single character
(+) `^` : Search the beginning of a line
(+) `$` : Search the end of a line
(+) `[abc]` : Search for a specified characters
(+) `[^abc]` : search for other characters, but not these
(+) `*` : Match zero or more of the rpeceding characters of expression.

(+) `grep + <regular expression> + <location> > <file to redirect>`
(+) `grep -v <regex> + <location> > <file to redirect>`: take everything except what found in regex