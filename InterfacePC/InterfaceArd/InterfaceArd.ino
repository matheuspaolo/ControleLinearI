#include <Wire.h>

String instrucao_rx, valor1_str, valor2_str, valor3_str, str_temp,velocidade_str,sentido_str;
String h;
int velocidade, qtd1,qtd2,qtd3;
float r1, r2, r3, lido_m1,lido_m2,lido_m3, r_ref,r1_lido,r2_lido,r3_lido;
char d;


void setup() {
	Serial.begin(9600);
	Wire.begin(8);
	pinMode(13, OUTPUT);
}

void loop() {

	if (Serial.available()) {
		char inst = Serial.read();
		if (inst == ',') {

			if (instrucao_rx == "receberv1") {
				str_temp = Serial.readString();
				valor1_str = str_temp;
				r1 = valor1_str.toFloat();
				Serial.print("-- Valor de R1 recebido: ");
				Serial.print(valor1_str);
				Serial.println(" ohms");
				str_temp = "";
			}

			if (instrucao_rx == "receberv2") {
				str_temp = Serial.readString();
				valor2_str = str_temp;
				r2 = valor2_str.toFloat();
				Serial.print("-- Valor de R2 recebido: ");
				Serial.print(valor2_str);
				Serial.println(" ohms");
				str_temp = "";
			}

			if (instrucao_rx == "receberv3") {
				str_temp = Serial.readString();
				valor3_str = str_temp;
				r3 = valor3_str.toFloat();
				Serial.print("-- Valor de R3 recebido: ");
				Serial.print(valor3_str);
				Serial.println(" ohms\n");
				str_temp = "";
			}

			if (instrucao_rx == "sentido") {
				str_temp = Serial.readString();
				sentido_str = str_temp;

				Wire.beginTransmission(8);
				Wire.write(sentido_str.c_str());
				Wire.endTransmission();

				Serial.print("-- Sentido de rotação desejado: ");
				Serial.println(sentido_str);
				str_temp = "";
			}

			if (instrucao_rx == "velocidade") {
				str_temp = Serial.readString();
				velocidade_str = str_temp;
				velocidade = velocidade_str.toInt();

				Wire.beginTransmission(8);
				Wire.write(velocidade_str.c_str());
				Wire.endTransmission();

				Serial.print("-- Velocidade desejada recebida: ");
				Serial.print(velocidade_str);
				Serial.println(" rpm\n");
				str_temp = "";
			}

			/////////
			instrucao_rx = "";
			/////////
		}
		else {
			instrucao_rx += inst;
		}
	}

	/////////// TRECHO PARA SERVO 1 ///////////
	/////////// FIM DO SERVO 1 ///////////

	/////////// TRECHO PARA SERVO 2 ///////////
	/////////// FIM DO SERVO 2 ///////////

	/////////// TRECHO PARA SERVO 3 ///////////
	/////////// FIM DO SERVO 3 ///////////
}
