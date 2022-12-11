@Template
Funcionalidade: Template
Simples template de BDD para uma calculadora que adiciona dois números

Cenário: Add two numbers
	Dado o primeiro número é 50
	E o segundo número é 70
	Quando os dois números são adicionados
	Então o resultado deve ser 120


Cenário: Outline: Add two numbers permutations
	Dado o primeiro número é <Primeiro número>
	E o segundo número é <Segundo número>
	Quando os dois números são adicionados
	Então o resultado deve ser <Resultado esperado>

Exemplos:
	| Primeiro número | Segundo número | Resultado esperado |
	| 0               | 0              | 0                  |
	| -1              | 10             | 9                  |
	| 6               | 9              | 15                 |